using System;

namespace TimeServerContest
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new HttpServer("localhost", 3567);
            server.Listen();
            Console.ReadLine();
        }
    }
}
