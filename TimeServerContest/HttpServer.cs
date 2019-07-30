using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace TimeServerContest
{
    class HttpServer
    {
        private HttpListener _httpListener;
        private Thread _serverThread;

        public HttpServer(string domain, int port)
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://{domain}:{port}/");
        }

        public void Listen()
        {
            _serverThread = new Thread(new ThreadStart(Run));
            _serverThread.Start();
        }

        private void Run()
        {
            //TODO: Handle exception 
            _httpListener.Start();

            Console.WriteLine("Server has been started");
            foreach (var prefix in _httpListener.Prefixes)
            {
                Console.WriteLine(prefix);
            }

            Console.WriteLine("Waiting for connections");
            while (_httpListener.IsListening)
            {
                //TODO: Make async server
                HttpListenerContext context = _httpListener.GetContext();

                HttpListenerResponse response = context.Response;
                var responseStr = $"{{\"time\":\"{DateTime.UtcNow}\"}}";

                byte[] buffer = Encoding.UTF8.GetBytes(responseStr);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "application/json";

                using (Stream output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
            }

            _httpListener.Close();
        }
    }
}
