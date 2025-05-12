using Copart.BLL.Models.VehicleModels;
using FluentValidation;

namespace Copart.BLL.Validators
{
    public class VehicleValidator : AbstractValidator<VehicleAddModel>
    {
        public VehicleValidator()
        {
            RuleFor(e => e.Damage).NotEmpty()
                .WithMessage("Damage is required.");
            RuleFor(e => e.Vin).NotEmpty()
                .WithMessage("Damage is required");
            RuleFor(e => e.Make).NotEmpty()
                .WithMessage("Make is required.");
            RuleFor(e => e.Model).NotEmpty();
        }
    }
}
