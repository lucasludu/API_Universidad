using System.ComponentModel.DataAnnotations;

namespace Application.DTOs._auth.Request
{
    public class AuthenticationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
