
using FluentValidation;
using Unitester_Service.Dtos.Auth;

namespace Unitester_Service.Validators.Dtos.Auth;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Telefon raqami yaroqsiz! M: +998aaBBBCCDD");

         RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Parol kuchli parol emas!");
    }
    
}
