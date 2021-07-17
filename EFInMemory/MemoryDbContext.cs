using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace EFInMemory
{
    public class MemoryDbContext : DbContext
    {        
        public MemoryDbContext(DbContextOptions<MemoryDbContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
                user.HasOne<UserAddress>(s => s.Address);
            });            

            modelBuilder.Entity<UserAddress>()
                .Property(p => p.Id).HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
        }
    }
}

