using AutoMapper;
using InvoiceApp.Data;
using InvoiceApp.Data.Contracts;
using InvoiceApp.Entities;
using InvoiceApp.Middlewares.Exceptions;
using InvoiceApp.Models.Models;
using InvoiceApp.Services.Contracts;

namespace InvoiceApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public InvoiceService(InvoiceAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public InvoiceDto CreateInvoice(CreateInvoiceDto dto)
        {
            var builder = new InvoiceBuilder();

            builder.setVendor(dto.Vendor);
            builder.setVendee(dto.Vendee);
            builder.setDate(new DateTimeOffset(DateTime.UtcNow, new TimeSpan(1, 0, 0)));
            builder.setNotes(dto.Note);
            builder.setProducts(dto.Products);
            builder.setPaymentMethod(dto.paymentMethod);
            builder.setInvoiceNumber();

            var invoice = builder.BuildInvoice();

            if (invoice == null)
            {
                throw new Exception("Coś poszło nie tak! Proszę spróbuj ponownie.");
            }

            var result = _mapper.Map<InvoiceDto>(invoice);

            // generowanie faktury do pdf

            if (result == null)
            {
                throw new Exception("Błąd wewnętrzny! Nie udało się utworzyć faktury!");
            }

            _dbContext.Invoices.Add(invoice);
            _dbContext.SaveChanges();

            return result;

        }

        public IEnumerable<InvoiceDto> GetAllInvoices(int userId)
        {
            var invoices = _dbContext.Invoices.Where(x => x.CreatedById == userId);
            var result = new List<InvoiceDto>();

            if (invoices == null)
            {
                throw new NotFoundException("Nie znaleziono faktury z podanym numerem w bazie danych");
            }

            foreach (var invoice in invoices)
            {
                var dto = _mapper.Map<InvoiceDto>(invoice);
                result.Add(dto);
            }

            if (result == null)
            {
                throw new Exception("Coś poszło nie tak! Proszę spróbuj ponownie.");
            }

            return result;
        }

        public InvoiceDto GetInvoice(string invoiceNumber)
        {
            var invoice = _dbContext.Invoices.FirstOrDefault(x => x.InvoiceNumber == invoiceNumber);

            if (invoice == null)
            {
                throw new NotFoundException("Nie znaleziono faktury z podanym numerem w bazie danych");
            }

            var result = _mapper.Map<InvoiceDto>(invoice);

            if (result == null)
            {
                throw new Exception("Coś poszło nie tak! Proszę spróbuj ponownie.");
            }

            return result;
        }

        public InvoiceDto UpdateInvoice(string invoiceNumber, ModifyInvoiceDto dto)
        {
            var invoice = _dbContext.Invoices.FirstOrDefault(x => x.InvoiceNumber == invoiceNumber);

            if (invoice == null)
            {
                throw new NotFoundException("Nie znaleziono faktury z podanym numerem w bazie danych");
            }

            // zaimplementuj do końca logikę modyfikacji faktury + generowanie do pdf







        }

        public void DeleteInvoice(string invoiceNumber)
        {
            var invoice = _dbContext.Invoices.FirstOrDefault(x => x.InvoiceNumber == invoiceNumber);

            if (invoice == null)
            {
                throw new NotFoundException("Nie znaleziono faktury z podanym numerem w bazie danych");
            }

            _dbContext.Invoices.Remove(invoice);
            _dbContext.SaveChanges();
        }
    }
}
