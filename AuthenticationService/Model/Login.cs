using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Model
{
    public class Login
    {
        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        public bool IsValid => !string.IsNullOrEmpty(Password) || !string.IsNullOrEmpty(Username);
    }
}
