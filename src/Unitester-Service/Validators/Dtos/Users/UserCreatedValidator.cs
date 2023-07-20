using FluentValidation;
using Unitester_Service.Comman.Helpers;
using Unitester_Service.Dtos.Users;
namespace Unitester_Service.Validators.Dtos.Users;

public class UserCreatedValidator : AbstractValidator<UserCreatedDto>
{
    public UserCreatedValidator()
    {
        RuleFor(dto => dto.FirsName).NotNull().NotEmpty().WithMessage("Ism kiritilishi kerak!")
            .MinimumLength(3).WithMessage("Ism 3ta belgidan ko'p kiritilishi kerak!")
            .MaximumLength(50).WithMessage("Ism 50ta belgidan kam kiritilishi kerak!");

     //   RuleFor(dto=>dto.Image).NotEmpty().NotNull().WithMessage()
        RuleFor(dto => dto.Image.Length).LessThan(5 * 1024).WithMessage("Rasm hajmi 5 MB dan kam bo'lishi kerak");
        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension) ;
        }).WithMessage("Bu fayl rasm fayllari turidan emas!") ;
    }
}
