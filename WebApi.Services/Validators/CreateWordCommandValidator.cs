using FluentValidation;
using WebApi.Domain.Models;

namespace WebApi.Services.Validators;

public sealed class CreateWordCommandValidator: AbstractValidator<CreateWordCommand>
{
    public CreateWordCommandValidator()
    {
        RuleFor(x => x.TextEn).NotEmpty();
        RuleFor(x => x.TextRu).NotEmpty();
    }
}