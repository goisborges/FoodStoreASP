using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodStore.Models
{
    public class Category
    {
        [Required]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="No empty strings allowed")]
        public string Name { get; set; }

        // child reference
        public List<Product> Products { get; set; }

    }
}
