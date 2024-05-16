using System.ComponentModel.DataAnnotations;

namespace Demo.Core.DTO
{
    public class LogenUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
