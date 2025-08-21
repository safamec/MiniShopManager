using Microsoft.EntityFrameworkCore;
using MiniShopManager.Models;

namespace MiniShopManager.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
