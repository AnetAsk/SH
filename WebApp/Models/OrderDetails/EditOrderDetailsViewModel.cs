using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.OrderDetails
{
    public class EditOrderDetailsViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public double Quantity { get; set; }

        public int OrderId { get; set; }
    }
}