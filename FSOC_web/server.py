from http.server import SimpleHTTPRequestHandler, ThreadingHTTPServer

class UnityWebGLHandler(SimpleHTTPRequestHandler):

    def end_headers(self):
        if self.path.endswith(".gz"):
            self.send_header("Content-Encoding", "gzip")

        super().end_headers()


server_address = ("localhost", 8000)

httpd = ThreadingHTTPServer(
    server_address,
    UnityWebGLHandler
)

print("FSOC WebGL server running at:")
print("http://localhost:8000")

httpd.serve_forever()