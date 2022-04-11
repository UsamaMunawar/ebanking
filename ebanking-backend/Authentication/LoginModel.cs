using System.ComponentModel.DataAnnotations;

namespace ebanking_backend.Authentication
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string AccountNumber { get; set; }
        public string Pincode { get; set; }
        public string Password { get; set; }
    }
}
