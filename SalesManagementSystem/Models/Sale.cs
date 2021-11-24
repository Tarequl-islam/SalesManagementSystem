using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesManagementSystem.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Customer Phone")]
        public string CustomerPhone { get; set; }

        [Required]
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Total Price")]
        public int TotalPrice { get; set; }

        [Required]
        public int Paid { get; set; }
        public int Due { get; set; }

    }
}