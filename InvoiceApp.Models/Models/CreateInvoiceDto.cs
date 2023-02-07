using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models.Models
{
    public class CreateInvoiceDto
    {
        public string Vendor { get; set; }
        public string Vendee { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Note { get; set; }
        public PaymentMethod paymentMethod { get; set; }
    }
}
