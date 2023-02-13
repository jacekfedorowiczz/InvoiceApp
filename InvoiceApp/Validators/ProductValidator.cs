using FluentValidation;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Produkt musi mieć swoją nazwę");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Produkt musi mieć swoją cenę.")
                .GreaterThanOrEqualTo(0).WithMessage("Cena produktu musi być większa lub równa 0.");

            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("Produkt musi mieć swoją ilość.")
                .GreaterThanOrEqualTo(0).WithMessage("Ilość produktu musi wynosić 0 lub więcej.");
        }
    }
}
