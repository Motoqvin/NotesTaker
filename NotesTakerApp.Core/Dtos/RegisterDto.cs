using System.ComponentModel.DataAnnotations;

namespace NotesTakerApp.Core.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[\w.-]{3,20}$", 
            ErrorMessage = "3-20 characters. Allowed: letters, digits, . _ -")]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, 
            ErrorMessage = "6-100 characters required.")]
        public string Password { get; set; } = string.Empty;
    }
}