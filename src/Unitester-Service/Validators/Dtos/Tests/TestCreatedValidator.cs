using FluentValidation;
using Unitester_Service.Dtos.Tests;

namespace Unitester_Service.Validators.Dtos.Tests;

public class TestCreatedValidator : AbstractValidator<TestCreatedDto>
{
    public TestCreatedValidator()
    {
        RuleFor(dto => dto.test).NotNull().NotEmpty().WithMessage("Savol kiritilishi kerak!");

        RuleFor(dto => dto.VariantA).NotNull().NotEmpty().WithMessage("Variant kiritilishi kerak!");

        RuleFor(dto => dto.VariantB).NotNull().NotEmpty().WithMessage("Variant kiritilishi kerak!");

        RuleFor(dto => dto.VariantC).NotNull().NotEmpty().WithMessage("Variant kiritilishi kerak!");

        RuleFor(dto => dto.VariantD).NotNull().NotEmpty().WithMessage("Variant kiritilishi kerak!");

        RuleFor(dto => dto.RightVariant).NotNull().NotEmpty().WithMessage("To'g'ri variant kiritilishi kerak!");
    }
}
