using FluentValidation.Results;

namespace Server.Utils;

public static class ValidationErrorsFormater
{
    public static string FormatToString(this IEnumerable<ValidationFailure> errors) =>
        string.Join(", ", errors.Select(error => error.ErrorMessage));
}