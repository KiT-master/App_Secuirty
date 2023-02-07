using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class Logging
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Website { get; set; }
    }
}
