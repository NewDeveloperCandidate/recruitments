namespace Library.Repositories.Context;

public interface IContext
{
    ICollection<BookEntity> Books { get; }
    ICollection<RegisterEntity> Register { get; }
    Task SaveAsync();
}