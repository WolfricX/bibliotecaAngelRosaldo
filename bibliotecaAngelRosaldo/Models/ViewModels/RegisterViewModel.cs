using System.ComponentModel.DataAnnotations;

namespace bibliotecaAngelRosaldo.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}