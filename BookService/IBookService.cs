namespace MinimalAPIService.BookService
{
    public interface IBookService
    {
        List<Book> GetBooks();

        Book? GetBook(int id);
    }
}
