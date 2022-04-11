using System.ComponentModel.DataAnnotations;

namespace ebanking_backend.Models
{
    public class Currency
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Short Name is required")]
        public string ShortName { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Conversion Rate is required")]
        public string ConversionRate { get; set; }
    }
}
