using WebApi.Domain.Abstractions.Repositories;

namespace WebApi.Domain.Abstractions;

public interface IUnitOfWork
{
    IWordRepository Words { get; }
    Task SaveChangesAsync();
}