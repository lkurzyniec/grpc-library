using System.Collections.Generic;

namespace HappyCode.GrpcSample.AuthorServer
{
    public static class MockData
    {
        public static IEnumerable<Author.AuthorMessages.Author> Authors => new List<Author.AuthorMessages.Author>
        {
            new Author.AuthorMessages.Author
            {
                Id = 1,
                FullName = "John Smith",
                Gender = Author.AuthorMessages.Gender.Male,
            },
            new Author.AuthorMessages.Author
            {
                Id = 2,
                FullName = "Ann Lee",
                Gender = Author.AuthorMessages.Gender.Female,
            },
            new Author.AuthorMessages.Author
            {
                Id = 3,
                FullName = "William Shelton",
                Gender = Author.AuthorMessages.Gender.Male,
            },
        };
    }
}
