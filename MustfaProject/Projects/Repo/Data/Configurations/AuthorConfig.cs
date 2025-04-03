using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data.Configurations
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        

        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b=> b.Description).IsRequired();

            builder.HasMany(A => A.Books).WithOne(b => b.Author).HasForeignKey(A => A.AuthorId);

        }
    }
}
