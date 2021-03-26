import os
import argparse
import cgi
import json
import sys
import signal
import threading
from urllib import parse
from http.server import HTTPServer, BaseHTTPRequestHandler, SimpleHTTPRequestHandler
from datetime import datetime, timedelta

EXPIRE_DELTA = timedelta(minutes=20)

# Token: SessionObj
Memory = {}

# Path to 'server.py'
Root = os.path.dirname(sys.argv[0]) or '.'

CREATED = 0; STANDBY = 1; GAME_STARTED = 2; GAME_ENDED = 3; FINISHED = 4

def get_state_str(state):
    if state == CREATED: return 'CREATED'
    if state == STANDBY: return 'STANDBY'
    if state == GAME_STARTED: return 'GAME_STARTED'
    if state == GAME_ENDED: return 'GAME_ENDED'
    if state == FINISHED: return 'FINISHED'

class SessionObj():
    def __init__(self, token, output_path):
        self.token = token
        self.output_path = output_path
        self.set_state(CREATED, datetime.now() + EXPIRE_DELTA)
    
    def set_state(self, state, expire_time):
        self.state = state
        self.expire_time = expire_time
        log("Session with token [{}] moved to state [{}]".format(self.token, get_state_str(self.state)))

class RequestHandler(SimpleHTTPRequestHandler):

    def do_CORS(self):
        self.send_response(200, "ok")
        self.send_header('Access-Control-Allow-Origin', '*')
        self.send_header('Access-Control-Allow-Methods', 'GET, POST, OPTIONS')
        self.send_header('Access-Control-Allow-Headers', '*')
        self.end_headers()

    def split_url(self, path):
        split = parse.urlsplit(path) 
        action = split.path
        query = dict(parse.parse_qsl(split.query))
        return action, query

    def do_GET(self):
        global config_json_bytes, config, Memory, EXPIRE_DELTA

        current_time = datetime.now()
        
        action, query = self.split_url(self.path)

        if 'token' not in query:
            self.send_error(400, 'Missing path parameter \"token\"')
            return

        token = query['token']
        
        if action == '/new':
            if token in Memory:
                self.send_error(400, 'Invalid token')
                return

            name = current_time.strftime('%Y-%m-%d_%H-%M-%S') 
            try:
                output_path = Root + "/output/" + name
                os.mkdir(output_path)
                # folder = name
            except OSError as e:
                # folder = 'recent'
                self.send_error(500, 'Could not create output folder')
                return
            self.do_CORS()
            self.wfile.write(config_json_bytes)
            log('New session created with token [{}]'.format(token))
            Memory[token] = SessionObj(token, output_path)

        elif action == '/updatestate':
            if token not in Memory:
                self.send_error(400, 'Invalid token')
                return
            
            if 'state' not in query:
                self.send_error(400, 'Missing path parameter \"state\"')
                return
            
            try:
                state = int(query['state'])
            except ValueError as e:
                log("ERROR: {}".format(e))
                self.send_error(400, 'Invalid state [{}]'.format(query['state']))
                return

            if state == GAME_STARTED:
                if 'maxgameseconds' not in query:
                    self.send_error(400, 'Missing path parameter \"maxgameseconds\"')
                    return
                try:
                    maxgameseconds = int(query['maxgameseconds']) * 2
                except:
                    self.send_error(400, 'Invalid maxgameseconds [{}]'.format(query['maxgameseconds']))
                    return

                Memory[token].set_state(state, current_time + timedelta(seconds=maxgameseconds))
            elif state == FINISHED:
                Memory[token].set_state(state, current_time)
            elif state in [CREATED, STANDBY, GAME_ENDED]:
                Memory[token].set_state(state, current_time + EXPIRE_DELTA)
            else:
                self.send_error(400, 'Invalid state')
                return
            
            self.do_CORS()

        elif action == '/' or action == '/Build' or action == '/TemplateData':
            return SimpleHTTPRequestHandler.do_GET(self)
        else:
            self.send_error(404)

    def do_POST(self):
        global Memory

        action, query = self.split_url(self.path)

        if action == '/output':
            if 'token' not in query:
                self.send_error(400, 'Missing path parameter \"token\"')
                return
            token = query['token']

            ctype, pdict = cgi.parse_header(self.headers.get('content-type'))

            if ctype != 'application/json':
                self.send_error(400, 'Content-Type must be application/json')
                return

            if 'filename' not in query:
                self.send_error(400, 'Missing path parameter \"filename\"')
                return

            length = int(self.headers.get('content-length'))
            message = self.rfile.read(length)
            fileName = query['filename']

            if token not in Memory:
                self.send_error(400, 'Invalid token')
                return
            
            session = Memory[token]

            with open(session.output_path + "/" + fileName, "wb") as f:
                f.write(message)
                log("Received output from session [{}], outputting to [{}]".format(token, output_path))

            self.do_CORS()
        else:
            self.send_error(404)

def start_up_check(cpath):
    global Root

    if not os.path.exists(Root + '/output'):
        os.mkdir(Root + "/output/")
        log("Generating ./output folder which will contain battery logs.")
   
    if not os.path.exists(cpath):
        log("ERROR: Config at " + cpath + " does not exist.")
        return False

    return True

def cleanup_loop(delta):
    log("Cleaning up sessions")
    
    current_time = datetime.now()

    marked_for_del = []
    for key, value in Memory.items():
        if value.state == FINISHED or value.expire_time <= current_time:
            marked_for_del.append(key)
            log("Deleting expired session with key [{}]".format(key))
    for key in marked_for_del:
        del Memory[key]

    threading.Timer(delta, cleanup_loop, args=[delta]).start()

def log(message):
    print("[{}] {}".format(datetime.now().strftime('%Y-%m-%d_%H-%M-%S'), message))

def validateJSON(jsonData):
    try:
        json.loads(jsonData)
    except ValueError as err:
        return False
    return True

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("file")
    parser.parse_args()
    args = parser.parse_args()
    config = args.file

    if not os.path.isfile(config):
        log("ERROR: path {} is not a file".format(config))
        exit()

    with open(config, 'r') as f:
        config_json = f.read()
        if validateJSON(config_json):
            config_json_bytes = config_json.encode()
        else:
            log("ERROR: path {} is not a valid JSON file".format(config))
            exit()

    if start_up_check(config):
        log("Using " + config)
        server = HTTPServer(('', 8000), RequestHandler)
        log("Server running on port 8000")

        cleanup_loop(60)

        #Ensures that Ctrl-C cleanly kills all spawned threads
        server.daemon_threads = True  

        # A custom signal handle to allow us to Ctrl-C out of the process
        def signal_handler(signal, frame):
            log("Exiting http server (Ctrl+C pressed)")
            try:
                if server: 
                    server.server_close()
            finally:
                exit(0)

        # Install the keyboard interrupt handler
        signal.signal(signal.SIGINT, signal_handler)

        # Now loop forever
        try:
            while True:
                sys.stdout.flush()
                server.serve_forever()
        except KeyboardInterrupt:
            pass

        server.server_close()