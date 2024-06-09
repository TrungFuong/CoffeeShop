using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class PayRate
    {
        [Required]
        [Key]
        public Guid PayRateId { get; set; }
        [Required]
        [MaxLength(255)]
        public string PayrateName{ get; set; }
        [Required]
        public decimal PayrateValue { get; set; }
        public ICollection<Salary> Salaries { get; set; }
    }
}
