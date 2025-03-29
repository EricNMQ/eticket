using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class EmailMarketingModel
    {
        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your full name")]
        public string Name { get; set; }
    }
}
