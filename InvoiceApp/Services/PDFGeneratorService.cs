using InvoiceApp.Entities;
using InvoiceApp.Services.Contracts;

namespace InvoiceApp.Services
{
    public class PDFGeneratorService : IPDFGenerator
    {
        private readonly InvoiceAppDbContext _dbContext;

        public PDFGeneratorService(InvoiceAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GenerateToPdf(string invoiceNumber)
        {
            // znajdź fakturę w systemie 



            // wygeneruj pdf


            // sprawdź zgodność


            // zwróć klientowi




            throw new NotImplementedException();
        }
    }
}
