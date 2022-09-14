namespace WebApi.Domain.Abstractions;

public interface IBaseRepository<T> where T : class
{
    ValueTask<T?> FetchByIdAsync(int id);
    Task<IEnumerable<T>> FetchAllAsync();
    Task CreateAsync(T entity);
    Task CreateRangeAsync(IEnumerable<T> items);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> items);
    Task DeleteAsync(int id);
    Task DeleteAsync(T entity); 
    Task DeleteRangeAsync(IEnumerable<T> items);
}