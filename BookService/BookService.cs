namespace MinimalAPIService.BookService
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = new List<Book>
            {
                new()
                {
                    Id = 1,
                    Title = "Dependency Injection in .NET",
                    Author = "Mark Seemann"
                },
                new()
                {
                    Id = 2,
                    Title = "C# in Depth",
                    Author = "Jon Skeet"
                },
                new()
                {
                    Id = 3,
                    Title = "Programming Entity Framework",
                    Author = "Julia Lerman"
                },
                new()
                {
                    Id = 4,
                    Title = "Programming WCF Services",
                    Author = "Juval Lowy and Michael Montgomery"
                }
            };
        }

        public List<Book> GetBooks()
        {
            return this._books;
        }

        public Book? GetBook(int id)
        {
            var book = this._books.FirstOrDefault(x => x.Id == id);

            return book;
        }
    }
}
