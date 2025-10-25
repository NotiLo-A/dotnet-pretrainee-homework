using FluentValidation;
using HW4.DTOs;

namespace HW4.Validators;

public class CreateBookValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Book title is required")
            .MaximumLength(300).WithMessage("Book title cannot exceed 300 characters");

        RuleFor(x => x.PublishedYear)
            .InclusiveBetween(1000, DateTime.Now.Year)
            .WithMessage($"Published year must be between 1000 and {DateTime.Now.Year}");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("AuthorId must be greater than 0");
    }
}

public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
{
    public UpdateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Book title is required")
            .MaximumLength(300).WithMessage("Book title cannot exceed 300 characters");

        RuleFor(x => x.PublishedYear)
            .InclusiveBetween(1000, DateTime.Now.Year)
            .WithMessage($"Published year must be between 1000 and {DateTime.Now.Year}");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("AuthorId must be greater than 0");
    }
}