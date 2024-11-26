namespace Library.Repositories.Context;

public class BookEntity
{
    public string Title { get; init; }
    public string Author { get; init; }
    public string Isbn { get; init; }
    public string[] Tags { get; init; }
}