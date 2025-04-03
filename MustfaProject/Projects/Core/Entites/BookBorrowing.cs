using Core.Entites;
using LibraryBackend.Models;

namespace Core.Entities
{
    public class BookBorrowing 
    {

        public string Id { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }

        public string BorrowingId { get; set; }
        public Borrowing Borrowing { get; set; }
    }
}