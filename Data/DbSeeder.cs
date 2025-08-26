using MiniShopManager.Models;
using System.Linq;

namespace MiniShopManager.Data
{
    public static class DbSeeder
    {
        public static void Seed(ShopContext context)
        {
            // Only seed if there are no users/items
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Password = "123456" // ⚠️ Plaintext, for testing only
                });

                // Optional: Add test users
                context.Users.Add(new User
                {
                    Name = "Test User",
                    Email = "user@example.com",
                    Password = "user123"
                });
            }

            // Optional: Seed some items

            context.SaveChanges();
        }
    }
}
