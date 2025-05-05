using FluentValidation;

namespace Application.Feautures._career.Commands.AddCareerCommands
{
    public class AddCareerValidator : AbstractValidator<AddCareerCommand>
    {
        public AddCareerValidator()
        {
            RuleFor(c => c.Request.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Request.DurationInYears)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
