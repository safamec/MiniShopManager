using Microsoft.AspNetCore.Mvc;
using MiniShopManager.Data;
using MiniShopManager.Models;
using System.Linq;
using Microsoft.AspNetCore.Http; // for HttpContext.Session

namespace MiniShopManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopContext _context;

        public AccountController(ShopContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
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

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            // 1) Hard-coded admin login (not stored in DB)
            if (email == "admin@gmail.com" && password == "123456")
            {
                HttpContext.Session.SetString("UserName", "Admin");
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Dashboard", "Admin");
            }

            // 2) Normal user login from DB
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("IsAdmin", "false");
                return RedirectToAction("LoginSuccess");
            }

            // 3) Invalid login
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        // Normal user login success page
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

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
