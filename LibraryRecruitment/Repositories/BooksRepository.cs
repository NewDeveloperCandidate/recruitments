using Library.Repositories.Context;

namespace Library.Repositories;

public class BooksRepository(IContext context) : IBooksRepository
{
    public Task<BookEntity[]> GetAllAsync()
    {
        return Task.FromResult(context.Books.ToArray());
    }

    public Task<int> GetAvailableCountAsync(string isbn)
    {
        var result = context.Register.Count(x => x.Isbn == isbn && x.IsAvailable);
        return Task.FromResult(result);
    }

    public Task<bool> CanLendAsync(string isbn)
    {
        var books = context.Books.Where(x => x.Isbn == isbn).ToArray();
        if (books.Length != 1)
        {
            return Task.FromResult(false);
        }

        var bookIsAvailable = context.Register.Any(x => x.Isbn == isbn && x.IsAvailable);
        return Task.FromResult(bookIsAvailable);
    }

    public async Task LendAsync(string isbn)
    {
        var book = context.Register.First(x => x.Isbn == isbn && x.IsAvailable);
        book.IsAvailable = false;

        await context.SaveAsync();
    }

    public async Task ReturnAsync(string isbn)
    {
        var book = context.Register.First(x => x.Isbn == isbn && !x.IsAvailable);
        book.IsAvailable = true;

        await context.SaveAsync();
    }
}