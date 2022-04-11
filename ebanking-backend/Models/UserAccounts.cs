using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ebanking_backend.Models
{
    public class UserAccounts
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username  { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string PinCode { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string Balance { get; set; }

        
    }
}
