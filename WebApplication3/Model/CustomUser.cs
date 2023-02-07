using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class CustomUser : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        public byte[]? CreditCardNo { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? MobileNo { get; set; }

        [Required]
        public string? DeliveryAddress { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public override string? Email { get; set; }

        [Required]
        public string? Photo { get; set; }

        public string? AboutMe { get; set; }

    }
}
