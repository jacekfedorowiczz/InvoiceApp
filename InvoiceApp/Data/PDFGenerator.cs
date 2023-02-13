using InvoiceApp.Data.Contracts;
using InvoiceApp.Entities;
using Aspose.Pdf;
using System.Reflection;

namespace InvoiceApp.Data
{
    public class PDFGenerator : IPDFGenerator
    {
        public void GenerateToPDF(Invoice invoice)
        {
            string dir = $@"..\Invoices\{invoice.CreatedById}\";
            Document document = new();
            Page page = document.Pages.Add();

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(invoice.InvoiceNo));
            







            document.Save(dir + $"{invoice.InvoiceNo}");
        }
    }
}
