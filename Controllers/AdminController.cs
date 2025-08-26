using Microsoft.AspNetCore.Mvc;
using MiniShopManager.Data;
using MiniShopManager.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MiniShopManager.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShopContext _context;

        public AdminController(ShopContext context)
        {
            _context = context;
        }

        // 🔒 Helper method to check admin status
        private bool IsAdmin()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            return isAdmin == "true";
        }

        // ✅ Dashboard: List all items
        public IActionResult Dashboard()
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account");

            var items = _context.Items.ToList();
            return View(items);
        }

        // ✅ Create item (GET)
        public IActionResult Create()
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account");

            return View();
        }

        // ✅ Create item (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item)
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account");

            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(item);
        }

        // ✅ Edit item (GET)
        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account");

            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ✅ Edit item (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account");

            if (ModelState.IsValid)
            {
                _context.Items.Update(item);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(item);
        }

        // ✅ Delete item
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account");

            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
    }
}
