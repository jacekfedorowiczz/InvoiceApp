using InvoiceApp.Entities;

namespace InvoiceApp.Data.Contracts
{
    public interface IPDFGenerator
    {
        void GenerateToPDF(Invoice invoice);
    }
}
