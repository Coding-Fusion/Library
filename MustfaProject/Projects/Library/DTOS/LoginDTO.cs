using System.ComponentModel.DataAnnotations;

namespace Library.DTOS
{
    public class LoginDTO
    {
        [Required]
        public string DispalyName { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
