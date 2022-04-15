using System.ComponentModel.DataAnnotations;

namespace TinkoffWatcher_Api.Models.Auth
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
