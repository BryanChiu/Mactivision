import os
import argparse
import cgi
import json
import urllib.parse
import re
import sys
from http.server import HTTPServer, BaseHTTPRequestHandler, SimpleHTTPRequestHandler
from os import walk
from datetime import datetime
from datetime import timedelta
from os import mkdir
import threading

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

    def do_OPTIONS(self):
        self.send_response(200, "ok")
        self.send_header('Access-Control-Allow-Origin', '*')
        self.send_header('Access-Control-Allow-Methods', 'GET, POST, OPTIONS')
        self.send_header('Access-Control-Allow-Headers', '*')
        self.end_headers()

    def do_GET(self):
        global folder, config, path, memory, EXPIRE_DELTA

        current_time = datetime.now()

        param_dict = urllib.parse.parse_qs(self.path)
        params = {}
        for key in param_dict:
            new_key = re.sub('^\/[A-Za-z]*\?', '', key)
            params[new_key] = param_dict[key][0]

        parsed_path = re.sub('^\/([A-Za-z]*)(\?.*)?$', r'\1', self.path)
        
        if 'token' not in params:
            self.send_response(400, 'Missing path parameter \"token\"')
            self.end_headers()
            return
        token = params['token']
        
        if parsed_path == 'new':
            if token in memory:
                self.send_response(400, 'Invalid token')
                self.end_headers()
                return

            name = current_time.strftime('%Y-%m-%d_%H-%M-%S') 
            try:
                output_path = path + "/output/" + name
                mkdir(output_path)
                # folder = name
            except OSError as e:
                # folder = 'recent'
                self.send_error(500)
                return
            self.do_OPTIONS()
            with open(config, 'rb') as f:
                self.wfile.write(f.read())
            memory[token] = SessionObj(CREATED, current_time + EXPIRE_DELTA, output_path)
        
        elif parsed_path == 'updatestate':
            if token not in memory:
                self.send_response(400, 'Invalid token')
                self.end_headers()
                return
            
            if 'state' not in params:
                self.send_response(400, 'Missing path parameter \"state\"')
                self.end_headers()
                return
            state = params['state']

            if state == GAME_STARTED:
                if 'maxgameseconds' not in params:
                    self.send_response(400, 'Missing path parameter \"maxgameseconds\"')
                    self.end_headers()
                    return
                maxgameseconds = params['maxgameseconds'] * 2
                memory[token].set_state(state, current_time + timedelta(seconds=maxgameseconds))
            elif state == FINISHED:
                memory[token].set_state(state, current_time)
            elif state in [CREATED, STANDBY, GAME_ENDED]:
                memory[token].set_state(state, current_time + EXPIRE_DELTA)
            else:
                self.send_response(400, 'Invalid state')
                self.end_headers()
                return
            
            self.do_OPTIONS()

        elif self.path == ('/') or self.path.startswith('/Build') or self.path.startswith('/TemplateData'):
            return SimpleHTTPRequestHandler.do_GET(self)
        else:
            self.send_error(404)

    def do_POST(self):
        global folder, path

        param_dict = urllib.parse.parse_qs(self.path)
        params = {}
        for key in param_dict:
            new_key = re.sub('^\/[A-Za-z]*\?', '', key)
            params[new_key] = param_dict[key][0]

        parsed_path = re.sub('^\/([A-Za-z]*)(\?.*)?$', r'\1', self.path)

        if parsed_path == 'output':
            ctype, pdict = cgi.parse_header(self.headers.get('content-type'))

            if ctype != 'application/json':
                self.send_response(400, 'Content-Type must be application/json')
                self.end_headers()
                return

            if 'filename' not in params:
                self.send_response(400, 'Missing path parameter \"filename\"')
                self.end_headers()
                return

            length = int(self.headers.get('content-length'))
            message = self.rfile.read(length)
            fileName = params['filename']

            session = memory[token]

            with open(session.output_path + "/" + fileName, "wb") as f:
                f.write(message)

            self.do_OPTIONS()

        else:
            self.send_response(404)
            self.end_headers()

def start_up_check(cpath):
    global path

    if not os.path.exists(path + '/output'):
        mkdir(path + "/output/")
        log(current_time, "Generating ./output folder which will contain battery logs.")
   
    if not os.path.exists(cpath):
        log(current_time, "ERROR: Config at " + cpath + " does not exist.")
        return False

    return True

def cleanup_loop():
    current_time = datetime.now()
    log(current_time, "Cleaning up sessions")
    for key, value in memory.items():
        
        # Cleanup memory
        if value.state == FINISHED:
            del memory[key]
            log(current_time, "Deleting finished session with key {}".format(key))

        elif value.expire_time <= current_time:
            del memory[key]
            log(current_time, "Deleting expired session with key {}".format(key))
    
    threading.Timer(60, cleanup_loop).start()

def log(time, message):
    print("[{}] {}".format(time.strftime('%Y-%m-%d_%H-%M-%S'), message))

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("file")
    parser.parse_args()
    args = parser.parse_args()
    config = args.file

    path = os.path.dirname(sys.argv[0]) or '.'

    current_time = datetime.now()

    if (start_up_check(config)):
        log(current_time, "Using " + config)
        server = HTTPServer(('', 8000), requestHandler)
        log(current_time, "Server running on port 8000")

        cleanup_loop()
        server.serve_forever()
