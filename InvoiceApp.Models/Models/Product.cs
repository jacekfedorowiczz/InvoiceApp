using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models.Models
{
    public class Product
    { 
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public Product()
        {
            Total = Amount * Price;
        }
    }
}
