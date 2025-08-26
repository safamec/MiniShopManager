using System.ComponentModel.DataAnnotations;

namespace MiniShopManager.Models
{
using System.ComponentModel.DataAnnotations.Schema;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public string ImageUrl { get; set; }

    // Add this back:
    public decimal Price { get; set; }

}


}
