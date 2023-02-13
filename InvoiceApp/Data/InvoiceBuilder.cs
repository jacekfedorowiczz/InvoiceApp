using InvoiceApp.Data.Contracts;
using InvoiceApp.Entities;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Data
{
    public class InvoiceBuilder : IInvoiceBuilder
    {
        private readonly Invoice _invoice = new();
        private readonly DateTime date = DateTime.Now;

        public InvoiceBuilder()
        {

        }

        public InvoiceBuilder setDate(DateTimeOffset date)
        {
            _invoice.CreateDate = date;
            return this;
        }

        public InvoiceBuilder setInvoiceNumber(int userId, int invoiceCount)
        {
            var invoiceNo = $"{userId}-{date.Month}-{date.Year}-{invoiceCount + 1}";

            _invoice.InvoiceNo = invoiceNo;
            return this;
        }

        public InvoiceBuilder setNotes(string note)
        {
            _invoice.Note = note;
            return this;
        }

        public InvoiceBuilder setPaymentMethod(PaymentMethod paymentMethod)
        {
            _invoice.PaymentMethod = paymentMethod;
            return this;
        }

        public InvoiceBuilder setProducts(IEnumerable<Product> products)
        {
            _invoice.Products = products;
            return this;
        }

        public InvoiceBuilder setVendee(string vendee)
        {
            _invoice.Vendee = vendee;
            return this;
        }

        public InvoiceBuilder setVendor(string vendor)
        {
            _invoice.Vendor = vendor;
            return this;
        }

        public Invoice BuildInvoice()
        {
            return _invoice;
        }
    }
}


