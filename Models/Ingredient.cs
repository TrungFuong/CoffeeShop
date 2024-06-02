using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Ingredient
    {
        [Required]
        [Key]
        public int IngredientId { get; set; }
        [Required]
        [MaxLength(255)]
        public string IngredientName { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string IngredientCondition { get; set; }
        
    }
}
