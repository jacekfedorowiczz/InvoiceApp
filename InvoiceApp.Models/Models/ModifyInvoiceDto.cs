using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models.Models
{
    public class ModifyInvoiceDto
    {
        public string InvoiceNo { get; set; }
        public string Vendor { get; set; }
        public Address VendorAddress { get; set; }
        public string Vendee { get; set; }
        public Address VendeeAddress { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Note { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
