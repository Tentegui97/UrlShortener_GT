using System.ComponentModel.DataAnnotations;

namespace UrlShortener_GT.Proyecto.Models
{
    public class AuthenticationRequestBody
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
