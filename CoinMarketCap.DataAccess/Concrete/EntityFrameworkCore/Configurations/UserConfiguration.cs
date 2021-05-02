using CoinMarketCap.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoinMarketCap.DataAccess.Concrete.EntityFrameworkCore.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).HasMaxLength(150).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(150);
            builder.Property(u => u.LastName).HasMaxLength(150);
        }
    }
}
