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
    public class BookReservationConfig : IEntityTypeConfiguration<BookReservation>
    {
        public void Configure(EntityTypeBuilder<BookReservation> builder)
        {

            builder.HasKey(br => new { br.BookId, br.ReservationId });

            builder.HasOne(br => br.book).WithMany(br => br.BookReservations).HasForeignKey(br => br.BookId);

            builder.HasOne(br => br.reservation).WithMany(br => br.BookReservations).HasForeignKey(br => br.ReservationId);



        }
    }
}
