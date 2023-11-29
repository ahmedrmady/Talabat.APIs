using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
       
        [Required]
        public string Password { get; set; }
       
        [Required]
        public string phoneNumber { get; set; }
       
        [Required]
        public string DisplayName { get; set; }
    }
}
