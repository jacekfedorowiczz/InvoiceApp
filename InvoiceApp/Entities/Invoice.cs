using InvoiceApp.Models.Models;

namespace InvoiceApp.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string Vendor { get; set; }
        public string Vendee { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Note { get; set; }
        public string InvoiceNo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
