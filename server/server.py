import os
import argparse
import cgi
import json
import sys
import threading
from urllib import parse
from http.server import HTTPServer, BaseHTTPRequestHandler, SimpleHTTPRequestHandler
from datetime import datetime, timedelta

EXPIRE_DELTA = timedelta(minutes=20)

# Token: SessionObj
memory = {}

CREATED = 0; STANDBY = 1; GAME_STARTED = 2; GAME_ENDED = 3; FINISHED = 4

class SessionObj():
    def __init__(self, state, expire_time, output_path):
        self.output_path = output_path
        self.set_state(state, expire_time)
    
    def set_state(self, state, expire_time):
        self.state = state
        self.expire_time = expire_time

class requestHandler(SimpleHTTPRequestHandler):

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
        global folder, config, path, memory, EXPIRE_DELTA

        current_time = datetime.now()
        
        action, query = self.split_url(self.path)

        if 'token' not in query:
            self.send_error(400, 'Missing path parameter \"token\"')
            return

        token = query['token']
        
        if action == '/new':
            if token in memory:
                self.send_error(400, 'Invalid token')
                return

            name = current_time.strftime('%Y-%m-%d_%H-%M-%S') 
            try:
                output_path = path + "/output/" + name
                os.mkdir(output_path)
                # folder = name
            except OSError as e:
                # folder = 'recent'
                self.send_error(500, 'Could not create output folder')
                return
            self.do_CORS()
            with open(config, 'rb') as f:
                self.wfile.write(f.read())
            memory[token] = SessionObj(CREATED, current_time + EXPIRE_DELTA, output_path)
            log('New session created with token [{}]'.format(token))

        elif action == '/updatestate':
            if token not in memory:
                self.send_error(400, 'Invalid token')
                return
            
            if 'state' not in params:
                self.send_error(400, 'Missing path parameter \"state\"')
                return
            
            state = int(params['state'])

            if state == GAME_STARTED:
                if 'maxgameseconds' not in params:
                    self.send_error(400, 'Missing path parameter \"maxgameseconds\"')
                    return
                maxgameseconds = params['maxgameseconds'] * 2
                memory[token].set_state(state, current_time + timedelta(seconds=maxgameseconds))
            elif state == FINISHED:
                memory[token].set_state(state, current_time)
            elif state in [CREATED, STANDBY, GAME_ENDED]:
                memory[token].set_state(state, current_time + EXPIRE_DELTA)
            else:
                self.error(400, 'Invalid state')
                return
            
            self.do_CORS()

        elif action == '/' or action == '/Build' or action == '/TemplateData':
            return SimpleHTTPRequestHandler.do_GET(self)
        else:
            self.send_error(404)

    def do_POST(self):
        global folder, path

        action, query = self.split_url(self.path)

        if action == '\output':
            if 'token' not in params:
                self.send_error(400, 'Missing path parameter \"token\"')
                return
            token = params['token']

            ctype, pdict = cgi.parse_header(self.headers.get('content-type'))

            if ctype != 'application/json':
                self.send_error(400, 'Content-Type must be application/json')
                return

            if 'filename' not in params:
                self.send_error(400, 'Missing path parameter \"filename\"')
                return

            length = int(self.headers.get('content-length'))
            message = self.rfile.read(length)
            fileName = params['filename']

            if token not in memory:
                self.send_error(400, 'Invalid token')
                return
            
            session = memory[token]

            with open(session.output_path + "/" + fileName, "wb") as f:
                f.write(message)

            self.do_CORS()
        else:
            self.send_error(404)

def start_up_check(cpath):
    global path

    if not os.path.exists(path + '/output'):
        os.mkdir(path + "/output/")
        log("Generating ./output folder which will contain battery logs.")
   
    if not os.path.exists(cpath):
        log("ERROR: Config at " + cpath + " does not exist.")
        return False

    return True

def cleanup_loop():
    log("Cleaning up sessions")
    
    for key, value in memory.items():
        if value.state == FINISHED or value.expire_time <= current_time:
            del memory[key]
            log("Deleting expired session with key [{}]".format(key))

    threading.Timer(60, cleanup_loop).start()

def log(message):
    print("[{}] {}".format(datetime.now().strftime('%Y-%m-%d_%H-%M-%S'), message))

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("file")
    parser.parse_args()
    args = parser.parse_args()
    config = args.file

    path = os.path.dirname(sys.argv[0]) or '.'

    if (start_up_check(config)):
        log("Using " + config)
        server = HTTPServer(('', 8000), requestHandler)
        log("Server running on port 8000")

        cleanup_loop()
        server.serve_forever()
