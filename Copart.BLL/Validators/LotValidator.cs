using Copart.BLL.Models.LotModels;
using FluentValidation;

namespace Copart.BLL.Validators
{
    public class LotValidator : AbstractValidator<LotAddModel>
    {
        public LotValidator()
        {
            RuleFor(e => e.Vehicle).NotNull()
                .WithMessage("Vehicle is required.");   
            RuleFor(e => e.StartDate).NotEmpty()
                .WithMessage("Start date is required");
            RuleFor(e => e.EndDate).NotEmpty()
                .WithMessage("End date is required.")
                .GreaterThan(e => e.StartDate)
                .WithMessage("End date must be greater than start date.");
        }
    }
}
