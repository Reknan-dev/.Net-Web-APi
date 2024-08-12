using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Features.User.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}


