using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class CheckTime
    {
        [Required]
        [Key]
        public Guid RecordId { get; set; }
        [Required]
        public DateTime CheckinTime { get; set; }
        [Required]
        public DateTime CheckoutTime { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public Employee Employee { get; set; }
    }
}