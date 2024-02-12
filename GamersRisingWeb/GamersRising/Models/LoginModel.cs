using System.ComponentModel.DataAnnotations;

namespace GamersRising.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [EmailAddress]
        public  string Email { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember me?")]
        public bool RememberMe { get; set; }
        public string? LoginInValid { get; set; }

    }
}
