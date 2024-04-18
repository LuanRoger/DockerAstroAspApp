using FluentValidation;
using Server.Models.Requests;

namespace Server.Validations;

public class UpdateClientValidator : AbstractValidator<UpdateClientRequest>
{
    public UpdateClientValidator()
    {
        RuleFor(f => f.newName)
            .NotEmpty();

        RuleFor(f => f.newEmail)
            .EmailAddress();
    }
}