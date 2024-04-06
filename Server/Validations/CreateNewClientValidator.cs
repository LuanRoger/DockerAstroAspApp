using FluentValidation;
using Server.Models.Requests;

namespace Server.Validations;

public class CreateNewClientValidator : AbstractValidator<CreateNewClientRequest>
{
    public CreateNewClientValidator()
    {
        RuleFor(f => f.name)
            .NotEmpty();

        RuleFor(f => f.email)
            .EmailAddress();
    }
}