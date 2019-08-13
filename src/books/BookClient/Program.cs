using System;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCode.GrpcSample.Book.BookMessages;
using HappyCode.GrpcSample.Common;
using static HappyCode.GrpcSample.Book.BookMessages.BookService;

namespace HappyCode.GrpcSample.BookClient
{
    public class Program
    {
        private const int PORT = 9000;

        private static BookServiceClient _client = new BookServiceClient(new Channel("LKURZYNIEC-WRO", PORT, SslCredentialsProvider.ClientCredentials));

        public static void Main(string[] args)
        {
            string option = string.Empty;
            while (option.ToLower() != "q")
            {
                Console.Write("What to do?: ");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        SendMetadataAsync().Wait();
                        break;

                    case "2":
                        GetAsync().Wait();
                        break;

                    default:
                        option = "q";
                        break;
                }
                Console.WriteLine("done!");
            }
        }

        private static async Task GetAsync()
        {
            var response = await _client.GetAsync(new BookRequest{ Isbn = "123" });
            Console.WriteLine($"{response.Book?.Id} : {response.Book?.Isbn} : {response.Book?.Title} : {response.Book?.AuthorCase}->{response.Book?.AuthorObj?.FullName ?? response.Book.AuthorId.ToString()}");
        }

        private static async Task SendMetadataAsync()
        {
            Metadata md = new Metadata();
            md.Add("username", "some_user_name");
            md.Add("password", "some_secret_pass");
            try
            {
                await _client.GetAsync(new BookRequest(), md);
            }
            catch(Exception)
            {
                // on purpose
            }
        }
    }
}
