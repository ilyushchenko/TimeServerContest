using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using TimeServerContest.Responses;

namespace TimeServerContest
{
    public delegate IHttpResult ResponseDelegate(Dictionary<string, string> parameters);
    class HttpServer
    {
        private readonly Dictionary<string, ResponseDelegate> _routes;
        private readonly HttpListener _httpListener;
        private Thread _serverThread;

        public HttpServer(string domain, int port)
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://{domain}:{port}/");
            _routes = new Dictionary<string, ResponseDelegate>();
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

                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                var route = request.Url.AbsolutePath.ToUpper();

                IHttpResult responseResult = new NotFoundResult();
                if (_routes.ContainsKey(route))
                {
                    var queryParams = new Dictionary<string, string>();
                    foreach (string key in request.QueryString.Keys)
                    {
                        if (key != null)
                        {
                            queryParams.Add(key, request.QueryString[key]);
                        }
                    }
                    responseResult = _routes[route].Invoke(queryParams);
                }

                response.ContentLength64 = responseResult.ContentLength;
                response.ContentType = responseResult.ContentType;
                response.StatusCode = responseResult.StatusCode;

                using (Stream output = response.OutputStream)
                {
                    output.Write(responseResult.Content, 0, responseResult.Content.Length);
                    output.Close();
                }
            }

            _httpListener.Close();
        }

        public void AddEndpoint(string uri, ResponseDelegate action)
        {
            if (uri is null)
                throw new ArgumentNullException(nameof(uri));
            if (uri.Length == 0 || uri[0] != '/')
                throw new ArgumentException("Path must start with /");

            _routes.Add(uri.ToUpper(), action);
        }
    }
}
