using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Database.Mappings;
using WebApi.Domain.Entities;

namespace WebApi.Database.Common;

public sealed class EnglishTimeContext : IdentityDbContext<UserEntity> 
{
    public EnglishTimeContext(DbContextOptions<EnglishTimeContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WordMap());
        base.OnModelCreating(modelBuilder);
    }
}