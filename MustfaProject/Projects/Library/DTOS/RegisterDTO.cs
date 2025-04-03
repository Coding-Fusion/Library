using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.DTOS
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$")]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
