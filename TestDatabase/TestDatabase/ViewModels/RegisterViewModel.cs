using System.ComponentModel.DataAnnotations;

namespace TestDatabase.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? username { get; set; }

        [Required]
        [EmailAddress]
        public string? email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? confirmPassword { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }


}