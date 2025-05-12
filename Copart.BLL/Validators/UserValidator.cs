using Copart.BLL.Models.UserModels;
using FluentValidation;

namespace Copart.BLL.Validators
{
    public class UserValidator : AbstractValidator<UserAddModel>
    {
        public UserValidator()
        {
            RuleFor(e => e.Name).NotEmpty()
                .WithMessage("Username is required.")
                .Length(3, 20)
                .WithMessage("Username must be between 3 and 20 characters.");
            RuleFor(e => e.Email).NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
        }
    }
}
