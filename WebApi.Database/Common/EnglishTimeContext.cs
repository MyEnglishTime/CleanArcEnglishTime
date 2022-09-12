using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;

namespace WebApi.Database.Common;

public sealed class EnglishTimeContext : DbContext
{
    public DbSet<WordEntity> Words { get; set; }
    
    public EnglishTimeContext(DbContextOptions<EnglishTimeContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}