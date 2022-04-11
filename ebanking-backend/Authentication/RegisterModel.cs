using System.ComponentModel.DataAnnotations;

namespace ebanking_backend.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "AccountNumber is required")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Pincode is required")]
        public string Pincode { get; set; }
    }
}
