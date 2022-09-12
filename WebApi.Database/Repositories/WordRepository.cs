using Microsoft.EntityFrameworkCore;
using WebApi.Database.Common;
using WebApi.Domain.Abstractions.Repositories;
using WebApi.Domain.Entities;

namespace WebApi.Database.Repositories;

public class WordRepository: BaseRepository<WordEntity>, IWordRepository
{
    private readonly DbSet<WordEntity> _dbset;
    public WordRepository(EnglishTimeContext englishTimeContext) : base(englishTimeContext)
    {
        _dbset = englishTimeContext.Set<WordEntity>();
    }
}