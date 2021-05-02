using CoinMarketCap.Core.Entities.Concrete;
using CoinMarketCap.DataAccess.Concrete.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CoinMarketCap.DataAccess.Concrete.EntityFrameworkCore.Contexts
{
    public class CoinMarketCapContext : DbContext
    {
        public CoinMarketCapContext(DbContextOptions<CoinMarketCapContext> options)
            : base(options)
        {
        }

        public CoinMarketCapContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
