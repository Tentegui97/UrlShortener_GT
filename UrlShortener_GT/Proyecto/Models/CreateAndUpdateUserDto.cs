using System.ComponentModel.DataAnnotations;

namespace UrlShortener_GT.Proyecto.Models
{
    public class CreateAndUpdateUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
