using Ecommerce.Features.User.Requests;
using FastEndpoints;
using FluentValidation;

namespace Ecommerce.Models.Entities
{
    public class Validator : Validator<CreateUserRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("your username is required!")
                .MinimumLength(3).WithMessage("username is too short!")
                .MaximumLength(25).WithMessage("username is too long!");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("email address is required!")
                .EmailAddress().WithMessage("the format of your email address is wrong!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("a password is required!")
                .MinimumLength(9).WithMessage("password is too short!")
                .MaximumLength(25).WithMessage("password is too long!");
        }
    }
}
