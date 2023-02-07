using InvoiceApp.Entities;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Data.Contracts
{
    public interface IInvoiceBuilder
    {
        InvoiceBuilder setVendor(string vendor);
        InvoiceBuilder setVendee(string vendee);
        InvoiceBuilder setDate(DateTimeOffset date);
        InvoiceBuilder setNotes(string note);
        InvoiceBuilder setProducts(IEnumerable<Product> products);
        InvoiceBuilder setPaymentMethod();
        InvoiceBuilder setInvoiceNumber();
        Invoice BuildInvoice();
    }
}
