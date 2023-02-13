using FluentValidation;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.Country)
                .NotEmpty();

            RuleFor(a => a.City)
                .NotEmpty();

            RuleFor(a => a.PostalCode)
                .NotEmpty()
                .MaximumLength(6);

            RuleFor(a => a.Street)
                .NotEmpty()
                .MaximumLength(60);
        }
    }
}
