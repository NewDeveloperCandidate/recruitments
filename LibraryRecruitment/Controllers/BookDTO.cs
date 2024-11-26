namespace Library.Controllers;

public record BookDTO(string Title, string Author, string Isbn, string[] Tags)
{
    public int AvailableCount { get; set; }
}