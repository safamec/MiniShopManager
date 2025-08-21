using Microsoft.AspNetCore.Mvc;
using MiniShopManager.Data;
using MiniShopManager.Models;
using System.Linq;

namespace MiniShopManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopContext _context;

        public AccountController(ShopContext context)
        {
            _context = context;
        }

        // ✅ Register (GET)
        public IActionResult Register()
        {
            return View();
        }

        // ✅ Register (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email already exists");
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // ✅ Login (GET)
        public IActionResult Login()
        {
            return View();
        }

        // ✅ Login (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                // Store user in session
                HttpContext.Session.SetString("UserName", user.Name);
                return RedirectToAction("LoginSuccess");
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        // ✅ Login Success Page
        public IActionResult LoginSuccess()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login");
            }

            ViewBag.UserName = userName;
            return View();
        }

        // ✅ Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}