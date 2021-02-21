import cgi
from http.server import HTTPServer, BaseHTTPRequestHandler
from os import walk
from datetime import datetime
from os import mkdir

folder = "recent"

class requestHandler(BaseHTTPRequestHandler):

    def send_get_headers(self):
        self.send_response(200)
        self.send_header('Access-Control-Allow-Headers', 'authorization')
        self.send_header('Access-Control-Allow-Methods', 'GET, POST')
        self.send_header('Access-Control-Allow-Origin', '*')
        self.end_headers()
    
    def do_GET(self):
        global folder;

        if self.path.endswith('/new'):
            self.send_get_headers()
            name = datetime.now().strftime('%Y-%m-%d_%H-%M-%S') 
            try:
                mkdir("./output/" + name)
                folder = name
            except OSError as e:
                folder = (e) 
            self.wfile.write(folder.encode())
        
        if self.path.endswith('/get'):
            self.send_get_headers()

            with open('./input/GeneratedTemplate.json', 'rb') as f:
                self.wfile.write(f.read())

    def do_POST(self):
        global folder;

        if self.path.endswith('/post'):
            ctype, pdict = cgi.parse_header(self.headers.get('content-type'))

            # NOTE: in case we need this again, just gonna leave it here.
            #pdict['boundary'] = bytes(pdict['boundary'], "utf-8")
            #content_len = int(self.headers.get('Content-length'))
            #pdict['CONTENT-LENGTH'] = content_len
           
            if ctype == 'multipart/form-data':
                form = cgi.FieldStorage(
                    fp=self.rfile,
                    headers=self.headers,
                    environ={'REQUEST_METHOD': 'POST',
                            'CONTENT_TYPE': self.headers['Content-Type'],
                            })
                fn = form['file'].filename
                data = form['file'].file.read()
                open("./output/" + folder + "/" + fn, "wb").write(data)

            self.send_response(200)
            self.send_header('content-type', 'text/html')
            self.end_headers()

if __name__ == '__main__':
    server = HTTPServer(('localhost', 8000), requestHandler)
    print("Server running on port 8000");
    server.serve_forever()
