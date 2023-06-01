using System.ComponentModel.DataAnnotations;

namespace Models_.DTOs
{
    public class JourneyDTO
    {
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
    }
}
