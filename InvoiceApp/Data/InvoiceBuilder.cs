using InvoiceApp.Data.Contracts;
using InvoiceApp.Entities;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Data
{
    public class InvoiceBuilder : IInvoiceBuilder
    {
        private Invoice _invoice = new();

        public InvoiceBuilder()
        {

        }

        public InvoiceBuilder setDate(DateTimeOffset date)
        {
            _invoice.Date = date;
            return this;
        }

        public InvoiceBuilder setInvoiceNumber()
        {
            // zaimplementować tworzenie numerów faktur
            throw new NotImplementedException();
        }

        public InvoiceBuilder setNotes(string note)
        {
            _invoice.Note = note;
            return this;
        }

        public InvoiceBuilder setPaymentMethod(PaymentMethod paymentMethod)
        {
            // switch na wartosci enuma z dto
            throw new NotImplementedException();
        }

        public InvoiceBuilder setProducts(IEnumerable<Product> products)
        {
            // zaimplementować dodawanie produktów do faktury
            throw new NotImplementedException();
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


