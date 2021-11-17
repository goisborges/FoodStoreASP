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
        [Required (ErrorMessage = "Please make sure the order total was entered")]

        public double Total { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Error")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Error")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="Please make sure the order date was entered")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please make sure the address was entered")]

        public string Address { get; set; }

        //Foreign Key
        [Required]
        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please make sure the city was entered")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please make sure the province was entered")]
        public string Province { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please make sure the postal code was entered")]
        [Display (Name ="Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        public string Phone { get; set; }

        [Display(Name ="Payment Code")]
        public string PaymentCode { get; set; }

        //child rel
        public List<OrderItem> OrderItems { get; set; }

    }
}
