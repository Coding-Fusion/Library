using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.Description).IsRequired().HasMaxLength(1000);

        builder.HasOne(b => b.Author)  
            .WithMany(a => a.Books)    
            .HasForeignKey(b => b.AuthorId)  
            .OnDelete(DeleteBehavior.Restrict);

        
        builder.HasOne(b => b.Category)  
            .WithMany(c => c.Books) 
            .HasForeignKey(b => b.CategoryId);  
    }
}
