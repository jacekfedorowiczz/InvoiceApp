namespace InvoiceApp.Services.Contracts
{
    public interface IPDFGenerator
    {
        void GenerateToPdf(string invoiceNumber);
    }
}
