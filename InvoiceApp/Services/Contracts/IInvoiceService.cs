using InvoiceApp.Models.Models;

namespace InvoiceApp.Services.Contracts
{
    public interface IInvoiceService
    {
        InvoiceDto CreateInvoice(CreateInvoiceDto dto);
        InvoiceDto GetInvoice(string invoiceNumber);
        IEnumerable<InvoiceDto> GetAllInvoices(int userId);
        InvoiceDto UpdateInvoice(string invoiceNumber, ModifyInvoiceDto dto);
        void DeleteInvoice(string invoiceNumber);
    }
}
