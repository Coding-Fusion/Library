using LibraryBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Data.Configurations
{
    internal class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(n => n.Message).HasMaxLength(1000);

            builder.HasOne(N => N.User).WithMany(N => N.notifications).HasForeignKey(N => N.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
