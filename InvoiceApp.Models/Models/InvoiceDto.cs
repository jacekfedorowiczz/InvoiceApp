using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Models.Models
{
    public class InvoiceDto
    {
        public string InvoiceNo { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string Vendor { get; set; }
        public Address VendorAddress { get; set; }
        public string Vendee { get; set; }
        public Address VendeeAddress { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Note { get; set; }
    }
}
