using FoodStore.Data;
using FoodStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        //GET: /Shop/ShopByCategory/5
        public IActionResult ShopByCategory(int id)
        {
            // get products in selected category
            var products = _context.Products.Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToList();

            //get name of selected category
            var category = _context.Categories.Find(id);
            ViewBag.Category = category.Name;

            return View(products);
        }

        [HttpPost]
        public IActionResult AddToCart(int ProductId, int Quantity)
        {
            // look up price
            var product = _context.Products.Find(ProductId);
            var price = product.Price;

            //set UserId of Cart
            var userId = GetUserId();


            //check if item is already in the cart
            var cartItem = _context.CartItems.SingleOrDefault(c => c.ProductId == ProductId && c.UserId == userId);

            if(cartItem != null)
            {
                cartItem.Quantity += Quantity;
                _context.CartItems.Update(cartItem);
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    Price = (double)price,
                    UserId = userId
                };
                //save to CartItems table in db
                _context.CartItems.Add(cartItem);
                
            }
            _context.SaveChanges();

            //load the page
            return RedirectToAction("Cart");
        }

        private string GetUserId()
        {
            // check session for an existing UserId for this user's cart
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // this is the user's 1st cart item
                var userId = "";
                if (User.Identity.IsAuthenticated)
                {
                    // user has logged in; use email
                    userId = User.Identity.Name;
                }
                else
                {
                    //user anonymous. generate a unique identifier
                    userId = Guid.NewGuid().ToString();
                    // or use this: userId = HttpContext.Session.Id;
                }

                // store userId in a session var
                HttpContext.Session.SetString("UserId", userId);
            }

            return HttpContext.Session.GetString("UserId");
        }

        //GET: /Shop/Cart
        public IActionResult Cart()
        {
            //identify the user from the session var
            var userId = HttpContext.Session.GetString("UserId");

            //load the cart items for this user from the db for display
            //inlcude join tables!!!
            var cartItems = _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId).ToList();

            return View(cartItems);
        }

        // GET: /Shop/RemoveFromCart
        //remove items from cart
        public IActionResult RemoveFromCart(int id)
        {
            var cartItem = _context.CartItems.Find(id);
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        //GET: /Shop/Checkout
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        //POST: /Shop/Checkout
        [Authorize]
        [HttpPost]
        public IActionResult Checkout([Bind("FirstName, LastName, Address, City, Province, PostalCode, Phone")] Order order)
        {
            //autofill total, date, user
            order.Total = 1;
            order.OrderDate = DateTime.Now;
            order.UserId = User.Identity.Name;
            order.Total = (from c in _context.CartItems
                           where c.UserId == order.UserId
                           select c.Quantity * c.Price).Sum();

            //save order to session so we can keep i in memory for asving once payment gets completed
            //HttpContext.Session.Set only saves string or int
            //external library can save object - SessionExtensions / SetObject/GetObject
            //serializes to JSON
            HttpContext.Session.SetObject("Order", order);

            return RedirectToAction("Payment");
            

        }
    }
}
