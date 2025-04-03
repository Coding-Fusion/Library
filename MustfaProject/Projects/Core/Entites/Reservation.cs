using Core.Entites.Identity;
using Core.Entites;

namespace LibraryBackend.Models
{
    public class Reservation : BaseEntity
    {
        public string? UserReservationName { get; set; }
        public DateTime? ReservationDate { get; set; } = DateTime.UtcNow;
        public string? Status { get; set; }

        // Navigation Properties
        public string? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<BookReservation> BookReservations { get; set; }
    }
}