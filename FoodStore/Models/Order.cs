using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Models
{
    public class Order
    {
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Range(0, 9999999)]
        [Required (ErrorMessage = "Please make sure the order total was entered"]

        public double Total { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage ="Please make sure the order date was entered")]
        public DateTime OrderDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please make sure the address was entered")]

        public string Address { get; set; }

        //Foreign Key
        [Required]
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please make sure the city was entered")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please make sure the province was entered")]
        public string Province { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please make sure the postal code was entered")]
        public string PostalCode { get; set; }

        [Required]
        public string Phone { get; set; }

        public string PaymentCode { get; set; }

        //child rel
        public List<OrderItem> OrderItems { get; set; }

    }
}
