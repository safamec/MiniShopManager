using System.ComponentModel.DataAnnotations;

namespace MiniShopManager.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
