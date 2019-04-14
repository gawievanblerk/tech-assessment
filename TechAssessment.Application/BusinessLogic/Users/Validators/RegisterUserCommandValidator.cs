using System;
using TechAssessment.Application.BusinessLogic.Users.Commands;
using FluentValidation;

namespace TechAssessment.Application.BusinessLogic.Users.Validators
{
  public class RehisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
  {
    public RehisterUserCommandValidator()
    {
      RuleFor(x => x.User.Username).NotEmpty().WithMessage("Username is required")
          .MaximumLength(20).WithMessage("Maximum length for username is 20 chars")
          .MinimumLength(3).WithMessage("Minimum length for username is 3 chars");
      RuleFor(x => x.User.FirstName).NotEmpty().WithMessage("First name is required")
          .MaximumLength(60).WithMessage("Maximum length for first name is 60 chars");
      RuleFor(x => x.User.LastName).NotEmpty().WithMessage("Last name is required")
          .MaximumLength(60).WithMessage("Maximum length for last name is 60 chars");
    }
  }
}
