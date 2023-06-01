using System.ComponentModel.DataAnnotations;

namespace PruebaNewShore.DTOs
{
    public class JourneyDTO
    {
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
    }
}
