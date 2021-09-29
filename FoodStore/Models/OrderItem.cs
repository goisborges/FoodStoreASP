using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        [Required]
        [Range(1, 9999999)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 9999999)]
        public double Price { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        //parent references
        public Product Product { get; set; }
        public Order Order { get; set; }

    }
}
