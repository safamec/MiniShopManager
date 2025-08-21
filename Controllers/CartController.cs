using Microsoft.AspNetCore.Mvc;
using MiniShopManager.Models;
using Newtonsoft.Json;

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
        public IActionResult Add([FromBody] Item item)
        {
            var cart = HttpContext.Session.GetString(SessionKey);
            var items = string.IsNullOrEmpty(cart) ? new List<Item>() : JsonConvert.DeserializeObject<List<Item>>(cart);

            items.Add(item);
            HttpContext.Session.SetString(SessionKey, JsonConvert.SerializeObject(items));

            return Json(new { success = true });
        }

        // POST: /Cart/Remove
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetString(SessionKey);
            var items = string.IsNullOrEmpty(cart) ? new List<Item>() : JsonConvert.DeserializeObject<List<Item>>(cart);

            var itemToRemove = items.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null) items.Remove(itemToRemove);

            HttpContext.Session.SetString(SessionKey, JsonConvert.SerializeObject(items));
            return RedirectToAction("Index");
        }

        // GET: /Cart/Checkout
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetString(SessionKey);
            var items = string.IsNullOrEmpty(cart) ? new List<Item>() : JsonConvert.DeserializeObject<List<Item>>(cart);

            if (!items.Any())
            {
                return RedirectToAction("Index"); // prevent checkout with empty cart
            }

            return View(items); // Show checkout page with cart items
        }

        // POST: /Cart/CheckoutConfirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutConfirm(string customerName, string address)
        {
            // Here you could save the order to a database if needed

            // Clear the cart after checkout
            HttpContext.Session.Remove(SessionKey);

            ViewBag.CustomerName = customerName;
            ViewBag.Address = address;

            return View("CheckoutSuccess");
        }
    }
}
