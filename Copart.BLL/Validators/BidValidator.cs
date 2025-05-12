using Copart.BLL.Models.BidModels;
using FluentValidation;

namespace Copart.BLL.Validators
{
    public class BidValidator : AbstractValidator<BidAddModel>
    {
        public BidValidator()
        {
            RuleFor(e => e.Amount).NotEmpty()
                .WithMessage("Bid amount is required.")
                .GreaterThan(0)
                .WithMessage("Bid amount must be greater than 0.");
        }
    }
}
