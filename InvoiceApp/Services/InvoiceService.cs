using AutoMapper;
using InvoiceApp.Authorization;
using InvoiceApp.Data;
using InvoiceApp.Data.Contracts;
using InvoiceApp.Entities;
using InvoiceApp.Middlewares.Exceptions;
using InvoiceApp.Models.Models;
using InvoiceApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPDFGenerator _pdfGenerator;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public InvoiceService(InvoiceAppDbContext dbContext, IMapper mapper, IPDFGenerator pdfGenerator, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _pdfGenerator = pdfGenerator;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public InvoiceDto CreateInvoice(CreateInvoiceDto dto, int userId)
        {
            var builder = new InvoiceBuilder();
            var invoiceCount = _dbContext.Invoices.Where(x => x.CreatedById == userId).Count();

            builder.setVendor(dto.Vendor)
                    .setVendee(dto.Vendee)
                    .setDate(new DateTimeOffset(DateTime.UtcNow, new TimeSpan(1, 0, 0)))
                    .setNotes(dto.Note)
                    .setProducts(dto.Products)
                    .setPaymentMethod(dto.paymentMethod)
                    .setInvoiceNumber(userId, invoiceCount);

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

        public List<InvoiceDto> GetAllInvoices(int userId)
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

            return result.ToList();
        }

        public InvoiceDto GetInvoice(int id)
        {
            var invoice = _dbContext.Invoices.FirstOrDefault(x => x.Id == id);

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

        public InvoiceDto UpdateInvoice(ModifyInvoiceDto dto, int userId)
        {
            var invoice = _dbContext.Invoices.FirstOrDefault(x => x.InvoiceNo == dto.InvoiceNo);

            if (invoice == null)
            {
                throw new NotFoundException("Nie znaleziono faktury z podanym numerem w bazie danych");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, invoice, new ResourceOperationRequirement(ResourceOperation.UPDATE)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Niestety nie masz dostępu do modyfikacji zasobu!");
            }

            invoice.Vendor = dto.Vendor;
            invoice.Vendee = dto.Vendee;
            invoice.Products = dto.Products;
            invoice.Note = dto.Note;
            invoice.ModifiedDate = new DateTimeOffset(DateTime.Now, new TimeSpan(2,0,0));
            invoice.PaymentMethod = dto.PaymentMethod;

            var updatedInvoiceDto = _mapper.Map<InvoiceDto>(invoice);

            if (updatedInvoiceDto == null)
            {
                throw new Exception("Coś poszło nie tak. Spróbuj ponownie później.");
            }

            var result = _pdfGenerator.GenerateToPDF(updatedInvoiceDto);

            _dbContext.Invoices.Update(invoice);
            _dbContext.SaveChanges();

            return result;
        }

        public void DeleteInvoice(int invoideId)
        {
            var invoice = _dbContext.Invoices.FirstOrDefault(x => x.Id == invoideId);

            if (invoice == null)
            {
                throw new NotFoundException("Nie znaleziono faktury z podanym numerem w bazie danych");
            }

            _dbContext.Invoices.Remove(invoice);
            _dbContext.SaveChanges();
        }
    }
}
