using System.ComponentModel.DataAnnotations;

namespace Demo.Core.DTO
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="Not Match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
