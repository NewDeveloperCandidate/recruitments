using Library.Repositories.Context;

namespace Library.Repositories;

public interface IBooksRepository
{
    Task<BookEntity[]> GetAllAsync();
    Task<int> GetAvailableCountAsync(string isbn);
    Task<bool> CanLendAsync(string isbn);
    Task LendAsync(string isbn);
    Task ReturnAsync(string isbn);
}