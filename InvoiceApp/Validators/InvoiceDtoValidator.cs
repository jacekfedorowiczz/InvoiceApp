using FluentValidation;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Validators
{
    public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceDtoValidator()
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

            RuleFor(d => d.PaymentMethod)
                .NotEmpty();
        }
    }
}
