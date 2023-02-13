using InvoiceApp.Models.Models;

namespace InvoiceApp.Data.Contracts
{
    public interface IPDFGenerator
    {
        void GenerateToPDF(InvoiceDto invoice);
    }
}
