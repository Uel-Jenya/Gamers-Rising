using System.ComponentModel.DataAnnotations;

namespace GamersRising.Models
{
    public class RegistrationModel
    {

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(500, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(500, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Birth Date")]
        [Required]
        public string BirthDate { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        //public bool AcceptTos { get; set; }
        public string? RegistrationInValid { get; set; }
    }
}
