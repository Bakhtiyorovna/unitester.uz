using FluentValidation;
using Unitester_Service.Dtos.Auth;
namespace Unitester_Service.Validators.Dtos.Auth;

public class RegisterValidator:AbstractValidator<RegisterDto>
{
	public RegisterValidator()
	{

        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Ism talab qilinadi!")
            .MaximumLength(30).WithMessage("Ism 30 ta belgidan oshmasligi kerak");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("Familiya talab qilinadi!")
            .MaximumLength(30).WithMessage("Familiya 30 ta belgidan oshmasligi kerak");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Telefon raqami yaroqsiz! M: +998aaBBBCCDD");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Parol kuchli parol emas!");
    }
}
