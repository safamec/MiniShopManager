using System;
using System.ComponentModel.DataAnnotations;

namespace MiniShopManager.Models
{
    public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Add this line
    public bool IsAdmin { get; set; } = false;
}

}
