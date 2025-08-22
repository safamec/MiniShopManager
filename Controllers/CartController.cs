using Microsoft.AspNetCore.Mvc;
using MiniShopManager.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MiniShopManager.Controllers
{
    public class CartController : Controller
    {
        private const string SessionKey = "Cart";

        // GET: /Cart
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString(SessionKey);
            var items = string.IsNullOrEmpty(cart) ? new List<Item>() : JsonConvert.DeserializeObject<List<Item>>(cart);
            return View(items);
        }

        // POST: /Cart/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Item item)
        {
            var cart = HttpContext.Session.GetString(SessionKey);
            var items = string.IsNullOrEmpty(cart) ? new List<Item>() : JsonConvert.DeserializeObject<List<Item>>(cart);

            var existingItem = items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Stock += 1; // increase quantity
            }
            else
            {
                item.Stock = 1; // first time adding
                items.Add(item);
            }

            HttpContext.Session.SetString(SessionKey, JsonConvert.SerializeObject(items));

            // ✅ Redirect straight to Checkout page instead of Index
            return RedirectToAction("Checkout");
        }

        // POST: /Cart/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetString(SessionKey);
            var items = string.IsNullOrEmpty(cart) ? new List<Item>() : JsonConvert.DeserializeObject<List<Item>>(cart);

            var itemToRemove = items.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }

            HttpContext.Session.SetString(SessionKey, JsonConvert.SerializeObject(items));
            return RedirectToAction("Index");
        }

        // GET: /Cart/Checkout
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetString(SessionKey);
            var items = string.IsNullOrEmpty(cart) ? new List<Item>() : JsonConvert.DeserializeObject<List<Item>>(cart);

            if (!items.Any())
                return RedirectToAction("Index");

            return View(items);
        }

        // POST: /Cart/CheckoutConfirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutConfirm(string customerName, string address)
        {
            // Save order logic here (optional)
            HttpContext.Session.Remove(SessionKey);

            ViewBag.CustomerName = customerName;
            ViewBag.Address = address;

            return View("CheckoutSuccess");
        }
    }
}
