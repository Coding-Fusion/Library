using Core.Entites;
using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Book : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }

    // Navigation Property (to Author)
    public Author? Author { get; set; }

    // Foreign Key Property (for Author)
    [ForeignKey(nameof(Author))]
    public string? AuthorId { get; set; }

    public Category? Category { get; set; }

    // Foreign Key Property (for Category)
    [ForeignKey(nameof(Category))]
    public string? CategoryId { get; set; }

    public string? ISBN { get; set; }
    public int? Quantity { get; set; }
    public int? AvailableQuantity { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation Property for many-to-many relationships
    public ICollection<BookBorrowing>? BookBorrowings { get; set; }
    public ICollection<BookReservation>? BookReservations { get; set; }
}
