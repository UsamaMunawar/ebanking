using Microsoft.AspNetCore.Identity;

namespace ebanking_backend.Authentication
{
    public class ApplicationUser: IdentityUser
    {
        public string AccountNumber { get; set; }
        public string Pincode { get; set; }
    }
}
