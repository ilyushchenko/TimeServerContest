using System;
using System.Collections.Generic;
using TimeServerContest.Responses;

namespace TimeServerContest
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new HttpServer("localhost", 3567);
            server.AddEndpoint("/Time", TimeProcessing);
            server.Listen();
            Console.ReadLine();
        }

        public static IHttpResult TimeProcessing(Dictionary<string, string> parameters)
        {
            return new OkJsonResult(new TimeModel());
        }
    }
}
