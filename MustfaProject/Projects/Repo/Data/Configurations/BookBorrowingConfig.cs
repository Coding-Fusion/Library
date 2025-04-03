using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repo.Data.Configurations
{
    public class BookBorrowingConfig : IEntityTypeConfiguration<BookBorrowing>
    {
        public void Configure(EntityTypeBuilder<BookBorrowing> builder)
        {
            // Composite Primary Key
            builder.HasKey(bb => new { bb.BookId, bb.BorrowingId });

            
            builder.HasOne(bb => bb.Book)
                   .WithMany(b => b.BookBorrowings)
                   .HasForeignKey(bb => bb.BookId);

            
            builder.HasOne(bb => bb.Borrowing)
                   .WithMany(b => b.bookBorrowings)
                   .HasForeignKey(bb => bb.BorrowingId);
        }
    }
}