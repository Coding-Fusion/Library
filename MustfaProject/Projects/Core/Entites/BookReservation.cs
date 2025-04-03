using LibraryBackend.Models;

public class BookReservation
{

    public string  Id { get; set; }
    public string BookTitle { get; set; }
    public string BookId { get; set; }
    public string ReservationId { get; set; }
    public Book book { get; set; }  // Navigation to Book
    public Reservation reservation { get; set; }  // Navigation to Reservation
}
