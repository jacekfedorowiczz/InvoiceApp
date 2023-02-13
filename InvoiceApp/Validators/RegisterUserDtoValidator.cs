using FluentValidation;
using InvoiceApp.Entities;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegiserUserDto>
    {
        public RegisterUserDtoValidator(InvoiceAppDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(8);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "Ten email jest zajęty.");
                    }
                });
        }
    }
}
