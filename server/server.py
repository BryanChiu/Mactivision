import os
import argparse
import cgi
import json
import urllib.parse
import re
from http.server import HTTPServer, BaseHTTPRequestHandler
from os import walk
from datetime import datetime
from os import mkdir

folder = "recent"
config = ""

class requestHandler(BaseHTTPRequestHandler):

    def do_OPTIONS(self):
        self.send_response(200, "ok")
        self.send_header('Access-Control-Allow-Origin', '*')
        self.send_header('Access-Control-Allow-Methods', 'GET, POST, OPTIONS')
        self.send_header('Access-Control-Allow-Headers', '*')
        self.end_headers()

    def do_GET(self):
        global folder, config
        self.do_OPTIONS()

        if self.path.endswith('/new'):
            name = datetime.now().strftime('%Y-%m-%d_%H-%M-%S') 
            try:
                mkdir("./output/" + name)
                folder = name
            except OSError as e:
                folder = (e) 
            self.wfile.write(folder.encode())
        elif self.path.endswith('/get'):
            with open('./input/' + config, 'rb') as f:
                self.wfile.write(f.read())
        else:
            self.send_response(404)
            self.end_headers()

    def do_POST(self):
        global folder

        param_dict = urllib.parse.parse_qs(self.path)
        params = {}
        for key in param_dict:
            new_key = re.sub('^\/[A-Za-z]*\?', '', key)
            params[new_key] = param_dict[key][0]

        parsed_path = re.sub('^\/([A-Za-z]*)(\?.*)?$', r'\1', self.path)

        if parsed_path == 'post':
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
            fileName = params['filename'] if 'filename' in params else 'test.json'

            with open("./output/" + folder + "/" + fileName, "wb") as f:
                f.write(message)

            self.do_OPTIONS()
        else:
            self.send_response(404)
            self.end_headers()

def start_up_check(config):
    cpath = "./input/" + config

    if not os.path.exists('./input'):
        print("ERROR: Missing ./input folder which should contain configuration files")
        return False

    if not os.path.exists('./output'):
        print("ERROR: Missing ./output folder which will contain battery logs.")
        return False
   
    if not os.path.exists(cpath):
        print("ERROR: Config at " + cpath + " does not exist.")
        return False

    return True

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("file")
    parser.parse_args()
    args = parser.parse_args()
    config = args.file

    if (start_up_check(config)):
        print("Using " + config)
        server = HTTPServer(('localhost', 8000), requestHandler)
        print("Server running on port 8000")
        server.serve_forever()
