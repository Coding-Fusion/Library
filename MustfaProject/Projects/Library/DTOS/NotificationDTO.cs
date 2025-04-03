namespace Library.DTOS
{
    public class NotificationDTO
    {

        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; } = false;
        public string? UserID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
