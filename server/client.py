import os
import sys
from http.server import HTTPServer, SimpleHTTPRequestHandler

if __name__ == '__main__':
    path = os.path.dirname(sys.argv[0]) or '.'
    os.chdir(path)

    address = ('', 3000)
    httpd = HTTPServer(address, SimpleHTTPRequestHandler)
    print("Client running on port 3000")
    httpd.serve_forever()
