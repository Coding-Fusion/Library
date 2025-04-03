using Core.Entites.Identity;
using Core.Entites;
using Core.Entities;

namespace LibraryBackend.Models
{
    public class Borrowing :BaseEntity
    {

        public string UserName { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
        public DateTime ReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; } 
        public string Status { get; set; } 
      
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<BookBorrowing> bookBorrowings { get; set; } = new List<BookBorrowing>();
    }
}