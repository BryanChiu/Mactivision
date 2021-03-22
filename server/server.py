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
from os import mkdir

folder = "recent"
config = ""
path = ""

class requestHandler(SimpleHTTPRequestHandler):

    def do_OPTIONS(self):
        self.send_response(200, "ok")
        self.send_header('Access-Control-Allow-Origin', '*')
        self.send_header('Access-Control-Allow-Methods', 'GET, POST, OPTIONS')
        self.send_header('Access-Control-Allow-Headers', '*')
        self.end_headers()

    def do_GET(self):
        global folder, config, path

        print(self.path)

        if self.path.endswith('/new'):
            name = datetime.now().strftime('%Y-%m-%d_%H-%M-%S') 
            try:
                mkdir(path + "/output/" + name)
                folder = name
            except OSError as e:
                folder = 'recent'
            self.do_OPTIONS()
            self.wfile.write(folder.encode())
        elif self.path.endswith('/get'):
            self.do_OPTIONS()
            with open(config, 'rb') as f:
                self.wfile.write(f.read())
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
            fileName = params['filename']

            with open(path + "/output/" + folder + "/" + fileName, "wb") as f:
                f.write(message)

            self.do_OPTIONS()
        else:
            self.send_response(404)
            self.end_headers()

def start_up_check(cpath):
    global path

    if not os.path.exists(path + '/output'):
        mkdir(path + "/output/")
        print("Generating ./output folder which will contain battery logs.")
   
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

    path = os.path.dirname(sys.argv[0]) or '.'

    if (start_up_check(config)):
        print("Using " + config)
        server = HTTPServer(('', 8000), requestHandler)
        print("Server running on port 8000")
        server.serve_forever()
