using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class Logging
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
