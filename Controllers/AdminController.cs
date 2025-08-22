 using Microsoft.AspNetCore.Mvc;
using MiniShopManager.Data;
using MiniShopManager.Models;
using System.Linq;

namespace MiniShopManager.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShopContext _context;

        public AdminController(ShopContext context)
        {
            _context = context;
        }

        // ✅ Dashboard: List all items
        public IActionResult Dashboard()
        {
            var items = _context.Items.ToList();
            return View(items);
        }

        // ✅ Create item (GET)
        public IActionResult Create()
        {
            return View();
        }

        // ✅ Create item (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item)
        {
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
            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // ✅ Edit item (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
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
