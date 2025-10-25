using FluentValidation;
using HW4.DTOs;

namespace HW4.Validators;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Author name is required")
            .MaximumLength(200).WithMessage("Author name cannot exceed 200 characters");

        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Date of birth cannot be in the future");
    }
}

public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Author name is required")
            .MaximumLength(200).WithMessage("Author name cannot exceed 200 characters");

        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Date of birth cannot be in the future");
    }
}