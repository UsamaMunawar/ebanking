using ebanking_backend.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ebanking_backend.Extensions
{
    public static class UserAccountExtensions
    {
        public static async Task<ApplicationUser> FindByAccountNumberPinAsync(this UserManager<ApplicationUser> um, string acNumber, string pincode)
        {
            return await um?.Users?.SingleOrDefaultAsync(x => x.AccountNumber == acNumber && x.Pincode == pincode);
        }
        public static async Task<ApplicationUser> FindByAccountNumberAsync(this UserManager<ApplicationUser> um, string acNumber)
        {
            return await um?.Users?.SingleOrDefaultAsync(x => x.AccountNumber == acNumber);
        }
    }
}
