using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Features.Identity
{
    public class RegisterRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
