using FluentValidation;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Aby się zalogować należy podać adres e-mail.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Aby się zalogować należy podać hasło.");
        }
    }
}
