using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodStore.Data;
using FoodStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            // check user is admin or the owner of this order
            if (!User.IsInRole("Administrator"))
            {
                return View(await _context.Orders.ToListAsync());
            }
            else
            {
                var orders = _context.Orders.Where(o => o.UserId == User.Identity.Name).ToListAsync();
                return View(orders);
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            
            // check user is admin or the owner of this order
            if (!User.IsInRole("Administrator"))
            {
                if(order.UserId != User.Identity.Name)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(order);
        }

       

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
