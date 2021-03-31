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

IP = ""
PORT = 8000
ROOT = os.path.dirname(sys.argv[0]) or '.' 
OUTPUT_PATH = ROOT + "/output/"
EXPIRE_TIME = timedelta(minutes=20)
SESSIONS = {}
CREATED = 0; GAME_STARTED = 1; GAME_ENDED = 2; FINISHED = 3
CONFIG_DATA = ""
CLEANUP_TIME = 60

class Session():
    def __init__(self, token, output_path):
        self.token = token
        self.output_path = output_path
        self.set_state(CREATED, datetime.now() + EXPIRE_TIME)
    
    def set_state(self, state, expire_time):
        self.state = state
        self.expire_time = expire_time
        log("Session with token [{}] moved to state [{}]".format(self.token, get_state_str(self.state)))

# helper for logging
def get_state_str(state):
    if state == CREATED:      return 'CREATED'
    if state == GAME_STARTED: return 'GAME_STARTED'
    if state == GAME_ENDED:   return 'GAME_ENDED'
    if state == FINISHED:     return 'FINISHED'

# helper function to improve logging
def log(message):
    print("[{}] {}".format(datetime.now().strftime('%Y-%m-%d_%H-%M-%S'), message))

# create a thread timer that checks for dead player sessions and removes them.
def cleanup_loop(delta):
    log("Cleaning up sessions")
    
    current_time = datetime.now()

    marked_for_del = []
    for key, value in SESSIONS.items():
        if value.state == FINISHED or value.expire_time <= current_time:
            marked_for_del.append(key)
            log("Deleting expired session with key [{}]".format(key))
    for key in marked_for_del:
        del SESSIONS[key]

    threading.Timer(delta, cleanup_loop, args=[delta]).start()

# custom request handler so we can overwrite post and get handlers
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

    def get_url_query(self, path):
        split = parse.urlsplit(path) 
        query = dict(parse.parse_qsl(split.query))
        return query

    def state_created(self, token, query):
        # create new battery folder
        name = datetime.now().strftime('%Y-%m-%d_%H-%M-%S') 
        try:
            folder = OUTPUT_PATH + name
            os.mkdir(folder)
        except OSError as e:
            self.send_error(500, 'Could not create new folder')
            return

        # push CORS to allow passing data
        self.do_CORS()

        # send config file
        self.wfile.write(CONFIG_DATA)
        log('New session created with token [{}]'.format(token))

        # create new player session, set state to CREATED
        SESSIONS[token] = Session(token, folder)

    def state_game_started(self, token, query):
        # get the max time the game can run
        if 'maxgameseconds' not in query:
            self.send_error(400, 'Missing path parameter \"maxgameseconds\"')
            return
        
        # check max time is a number
        try:
            maxgameseconds = int(query['maxgameseconds']) * 2
        except:
            self.send_error(400, 'Invalid maxgameseconds [{}]'.format(query['maxgameseconds']))
            return

        # update player session state to GAME_STARTED
        SESSIONS[token].set_state(GAME_STARTED, datetime.now() + timedelta(seconds=maxgameseconds))

        # OK
        self.do_CORS()

    def post_data(self, token, query):
        ctype, pdict = cgi.parse_header(self.headers.get('content-type'))
        
        # make sure data sent over post is json
        if ctype != 'application/json':
           self.send_error(400, 'Content-Type must be application/json')
           return 

        # query has to have a filename for the writing the data
        if 'filename' not in query:
            self.send_error(400, 'Missing path parameter \"filename\"')
            return
  
        filename = query['filename']

        # read the post data
        data = self.rfile.read(int(self.headers.get('content-length')))

        # write file to server
        path = SESSIONS[token].output_path + "/" + filename
        with open(path, "wb") as f:
            f.write(data)
            log("Received output from session [{}], outputting to [{}]".format(token, path))

    def state_game_ended(self, token, query):
        # read and write post data that was sent
        self.post_data(token, query)

        # update state
        SESSIONS[token].set_state(GAME_ENDED, datetime.now() + EXPIRE_TIME)

        # OK
        self.do_CORS()

    def state_game_finished(self, token, query):
        # read and write post data that was sent
        self.post_data(token, query)

        # update state to finished
        SESSIONS[token].set_state(FINISHED, datetime.now())

        # OK
        self.do_CORS()

    def handle_request(self, request, query):
        # token must be url query
        if 'token' not in query:
            self.send_error(400, 'Missing path parameter \"token\"')
            return

        token = query['token']

        # state must be state query
        if 'state' not in query:
            self.send_error(400, 'Missing path parameter \"state\"')
            return
       
        # check state is a number
        try:
            state = int(query['state'])
        except:
            self.send_error(400, 'Bad state type [{}]'.format(query['state']))
            return

        # check state is valid
        if state not in [CREATED, GAME_STARTED, GAME_ENDED, FINISHED]:
            self.send_error(400, 'Bad state value [{}]'.format(query['state']))
            return

        # check if token in session except when state is CREATED
        if token not in SESSIONS and state != CREATED:
            self.send_error(400, 'Invalid token')
            return

        # do_GET
        if request == "get":
            if state == CREATED:
                self.state_created(token, query)
                return
            elif state == GAME_STARTED:
                self.state_game_started(token, query)
                return
            else:
                log("ERROR: Bad state in POST");

        # do_POST
        elif request == "post":
            if state == GAME_ENDED:
                self.state_game_ended(token, query)
                return
            elif state == FINISHED:
                self.state_game_finished(token, query)
                return
            else:
                log("ERROR: Bad state in POST");
        else:
            log("ERROR: Bad Request")

        # if no request is caught just return a 404
        self.send_error(404)
            
    def do_GET(self):
        action, query = self.split_url(self.path)
        
        if action == '/connect':
            self.handle_request("get", query)
        elif action == '/' or action.startswith('/Build') or action.startswith('/TemplateData'):
            return SimpleHTTPRequestHandler.do_GET(self)
        else:
            self.send_error(404)

    def do_POST(self):
        action, query = self.split_url(self.path)
          
        if action == '/connect':
            self.handle_request("post", query)
        else:
            self.send_error(404)

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("file")
    parser.parse_args()
    args = parser.parse_args()
    config = args.file
   
    # does the output folder exist?
    if not os.path.exists(OUTPUT_PATH):
        log("ERROR: Config at " + OUTPUT_PATH + " does not exist.")
        exit()

    # does the config file exist?
    if not os.path.isfile(config):
        log("ERROR: path {} is not a file".format(config))
        exit()

    # if the config file exists is it valid json?
    with open(config, 'r') as f:
        config_json = f.read()
        try:
            json.loads(config_json)
        except ValueError as err:
            log("ERROR: path {} is not a valid JSON file".format(config))
            exit()
    
    log("Using " + config)

    # convert json config to bytes
    CONFIG_DATA = config_json.encode()

    # start session cleanup routine
    cleanup_loop(CLEANUP_TIME)


    # a custom signal handle to allow us to Ctrl-C out of the process
    def signal_handler(signal, frame):
        log("Exiting http server (Ctrl+C pressed)")
        os._exit(0)

    # install the keyboard interrupt handler
    signal.signal(signal.SIGINT, signal_handler)

    # create server
    server = HTTPServer((IP, PORT), RequestHandler)
    log("Server running on {}:{}".format(IP,PORT))

    # ensures that Ctrl-C cleanly kills all spawned threads
    server.daemon_threads = True  
    
    # start serving
    server.serve_forever()
