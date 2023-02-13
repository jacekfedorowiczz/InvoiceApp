using InvoiceApp.Models.Models;

namespace InvoiceApp.Services.Contracts
{
    public interface IInvoiceService
    {
        InvoiceDto CreateInvoice(CreateInvoiceDto dto, int userId);
        InvoiceDto GetInvoice(int id);
        List<InvoiceDto> GetAllInvoices(int userId);
        InvoiceDto UpdateInvoice(ModifyInvoiceDto dto, int userId);
        void DeleteInvoice(int invoideId);
    }
}
