using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCode.GrpcSample.Author.AuthorMessages;

namespace HappyCode.GrpcSample.AuthorServer
{
    public class AuthorService : Author.AuthorMessages.AuthorService.AuthorServiceBase
    {
        public override Task<AuthorResponse> Get(AuthorRequest request, ServerCallContext context)
        {
            Metadata md = context.RequestHeaders;
            foreach (var item in md)
            {
                System.Console.WriteLine($"{item.Key} : {item.Value}");
            }

            return Task.FromResult(new AuthorResponse { Author = MockData.Authors.FirstOrDefault(x => x.Id == request.Id) });
        }

        public override async Task GetWithFilter(AuthorFilterRequest request, IServerStreamWriter<AuthorFilterResponse> responseStream, ServerCallContext context)
        {
            if (request.Id > 0)
            {
                await responseStream.WriteAsync(new AuthorFilterResponse { Author = MockData.Authors.FirstOrDefault(x => x.Id == request.Id) });
            }
            else
            {
                var query = MockData.Authors;
                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    query = query.Where(x => x.FullName.StartsWith(request.FullName));
                }
                if (request.Gender != Gender.Unknown)
                {
                    query = query.Where(x => x.Gender == request.Gender);
                }

                foreach (var item in query)
                {
                    await responseStream.WriteAsync(new AuthorFilterResponse { Author = item });
                }
            }
        }
    }
}
