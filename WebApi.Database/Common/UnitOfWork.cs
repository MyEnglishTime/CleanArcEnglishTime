using WebApi.Database.Repositories;
using WebApi.Domain.Abstractions;
using WebApi.Domain.Abstractions.Repositories;

namespace WebApi.Database.Common;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly EnglishTimeContext _englishTimeContext;

    public UnitOfWork(EnglishTimeContext englishTimeContext)
    {
        _englishTimeContext = englishTimeContext;
    }

    private IWordRepository _lazyUserRepository;
    public IWordRepository Words => _lazyUserRepository ??= new WordRepository(_englishTimeContext);

    public Task SaveChangesAsync() => _englishTimeContext.SaveChangesAsync();
}