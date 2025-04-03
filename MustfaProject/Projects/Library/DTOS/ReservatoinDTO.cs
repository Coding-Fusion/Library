namespace Library.DTOS
{
    public class ReservatoinDTO
    {

        public string UserReservationName { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public string UserId { get; set; }

    }
}
