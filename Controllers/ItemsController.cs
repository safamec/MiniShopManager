using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniShopManager.Data;
using MiniShopManager.Models;

namespace MiniShopManager.Controllers
{
public class ItemsController : Controller
{
    private readonly ShopContext _context;

    public ItemsController(ShopContext context)
    {
        _context = context;
    }

    // GET: Items
    public async Task<IActionResult> Index()
    {
        return View(await _context.Items.ToListAsync());
    }

    // GET: Items/Details/5  (optional)
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var item = await _context.Items.FirstOrDefaultAsync(m => m.Id == id);
        if (item == null)
            return NotFound();

        return View(item);
    }

    // (Optional) Admin add product page
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,ImageUrl")] Item item)
    {
        if (ModelState.IsValid)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}

}
