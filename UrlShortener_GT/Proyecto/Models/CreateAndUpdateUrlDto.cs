using System.ComponentModel.DataAnnotations;
using UrlShortener_GT.Proyecto.Entities;

namespace UrlShortener_GT.Proyecto.Models
{
    public class CreateAndUpdateUrlDto
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public User? User { get; set; }

    }
}
