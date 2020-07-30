using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Order
{
    public class CreateOrderViewModel
    {
        public int CustomerId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}