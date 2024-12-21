using FluentValidation.Results;

namespace SchoolManagerModel.Utils;

public static class ValidationErrors
{
    public static string GetErrorsFormatted(ValidationResult validationResult)
    {
        return string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
    }
}
