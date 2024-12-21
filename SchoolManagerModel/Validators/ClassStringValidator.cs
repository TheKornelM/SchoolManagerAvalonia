using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class ClassStringValidator : AbstractValidator<(string year, string cls)>
{
    public ClassStringValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x.year)
            .SetValidator(new ClassYearStringValidator(resourceManager));
        RuleFor(x => x.cls)
            .SetValidator(new CharLetterValidator(resourceManager));
    }
}