using Grpc.Core;
using System;
using HappyCode.GrpcSample.Common;

namespace HappyCode.GrpcSample.AuthorServer
{
    class Program
    {
        private const int PORT = 9001;

        static void Main(string[] args)
        {
            var server  =  new Server
            {
                Ports = { new ServerPort("0.0.0.0", PORT, SslCredentialsProvider.ServerCredentials) },
                Services = { Author.AuthorMessages.AuthorService.BindService(new AuthorService()) }
            };
            server.Start();

            Console.WriteLine($"Starting server on {PORT} port");
            Console.WriteLine("Press any key to stop...");
            Console.Read();

            server.ShutdownAsync().Wait();
        }
    }
}
