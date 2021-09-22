using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodStore.Models;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodStore.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var categories = new List<Category>();

            for (var i = 1; i < 11; i++)
            {
                categories.Add( new Category(){
                    CategoryId = i, Name = "Category " + i.ToString()});

            }

            return View(categories);
        }

        //Categories/browse?category=abc
        public IActionResult Browse(string category)
        {
            //ensure we have a category value
            if (category == null)
            {
                //redirect the user to Index so they can choose a category
                return RedirectToAction("Index");
            }
            //store the input parameter ina  var inside the ViewBag so we can display the user's selection
            ViewBag.category = category;

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
