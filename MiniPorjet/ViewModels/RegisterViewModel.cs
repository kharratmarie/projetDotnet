using System.ComponentModel.DataAnnotations;
namespace MiniPorjet.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // Ajout du champ téléphone
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Telephone { get; set; }

    }
}