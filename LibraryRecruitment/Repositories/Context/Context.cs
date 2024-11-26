namespace Library.Repositories.Context;

public class Context : IContext
{
    public ICollection<BookEntity> Books { get; init; } =
    [
        new()
        {
            Title = "Alice's Adventures in Wonderland",
            Author = "Lewis Carroll",
            Isbn = "9788377797662",
            Tags = ["children's literature", "fantasy", "literary nonsense"]
        },
        new()
        {
            Title = "The Fellowship of the Ring",
            Author = "J. R. R. Tolkien",
            Isbn = "9788372005458",
            Tags = ["fantasy"]
        },
        new()
        {
            Title = "The Master and Margarita",
            Author = "Mikhail Bulgakov",
            Isbn = "1784871931",
            Tags = ["fantasy", "farce", "romance"]
        }
    ];

    public ICollection<RegisterEntity> Register { get; init; } =
    [
        new()
        {
            Isbn = "9788377797662",
            IsAvailable = true
        },
        new()
        {
            Isbn = "9788377797662",
            IsAvailable = true
        },
        new()
        {
            Isbn = "9788377797662",
            IsAvailable = true
        },
        new()
        {
            Isbn = "9788377797662"
        },
        new()
        {
            Isbn = "9788377797662"
        },
        new()
        {
            Isbn = "9788372005458",
            IsAvailable = true
        },
        new()
        {
            Isbn = "9788372005458",
            IsAvailable = true
        },
        new()
        {
            Isbn = "9788372005458"
        },
        new()
        {
            Isbn = "1784871931",
            IsAvailable = true
        },
        new()
        {
            Isbn = "1784871931"
        }
    ];

    public Task SaveAsync()
    {
        return Task.CompletedTask;
    }
}