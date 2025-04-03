using Core.Entites.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(U => U.UserName).IsRequired();
            builder.Property( u => u.Email).IsRequired();

            builder.HasMany(u => u.notifications).WithOne(u => u.User).HasForeignKey(u => u.UserId);

            builder.HasMany(u => u.borrowing).WithOne(u => u.User).HasForeignKey(u => u.UserId);

            builder.HasMany( u => u.reservations ).WithOne(u => u.User).HasForeignKey(u => u.UserId);


        }
    }
}
