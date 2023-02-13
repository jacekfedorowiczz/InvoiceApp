using FluentValidation;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Validators
{
    public class CreateInvoiceDtoValidator : AbstractValidator<CreateInvoiceDto>
    {
        public CreateInvoiceDtoValidator()
        {
            RuleFor(d => d.Vendor)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(d => d.VendorAddress)
                .NotEmpty();

            RuleFor(d => d.Vendee)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(d => d.VendeeAddress)
                .NotEmpty();

            RuleFor(d => d.Products)
                .NotEmpty();

            RuleFor(d => d.paymentMethod)
                .NotEmpty();
        }
    }
}
