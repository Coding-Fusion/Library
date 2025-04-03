using LibraryBackend.Models;

namespace Library.DTOS
{
    public class UserDTO
    {

        public string DisplayName { get; set; }
        public string Email { get; set; }
        public ICollection<Notification> notifications { get; set; } = new List<Notification>();
        public ICollection<Borrowing> borrowing { get; set; } = new List<Borrowing>();
        public ICollection<Reservation> reservations { get; set; } = new List<Reservation>();
    }
}
