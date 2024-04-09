using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

namespace MinimalAPIService.BookService
{
    public static class BookModule
    {
        public static void AddBookServiceEndpoints(this IEndpointRouteBuilder app)
        {

            app.MapGet("/books", (IBookService bookService) => TypedResults.Ok(bookService.GetBooks()))
                .WithName("GetBooks")
                .WithOpenApi(x => new OpenApiOperation(x)
                {
                    Summary = "Get Library Books",
                    Description = "Returns information about all the available books from the library.",
                    Tags = new List<OpenApiTag> { new() { Name = "Library Service" } }
                });

            app.MapGet("/books/{id}", Results<Ok<Book>, NotFound> (IBookService bookService, int id) =>
                    bookService.GetBook(id) is { } book
                        ? TypedResults.Ok(book)
                        : TypedResults.NotFound()
                )
                .WithName("GetBookById")
                .WithOpenApi(x => new OpenApiOperation(x)
                {
                    Summary = "Get Library Book By Id",
                    Description = "Returns information about selected book from the library.",
                    Tags = new List<OpenApiTag> { new() { Name = "Library Service" } }
                });

            app.MapPost("/books", (Book book) =>
            {
                return book;
            })
            .WithName("CreateBook")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Add a new book",
                Description = "Add a new book to the library.",
                Tags = new List<OpenApiTag> { new() { Name = "Library Service" } }
            });

            app.MapPut("/books/{id}", (int id, Book book) =>
            {
                return book;
            })
            .WithName("UpdateBook")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Update an existing book by Id",
                Description = "Update an existing book in the library.",
                Tags = new List<OpenApiTag> { new() { Name = "Library Service" } }
            });

            app.MapDelete("/books/{id}", (int id) =>
            {
                return Results.NoContent();
            })
            .WithName("DeleteBook")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Delete an existing book by Id",
                Description = "Delete an existing book in the library.",
                Tags = new List<OpenApiTag> { new() { Name = "Library Service" } }
            });
        }
    }
}
