using TechAssessment.Application.BusinessLogic.Users.Commands;
using FluentValidation;
using TechAssessment.Application.BusinessLogic.Entries.Commands;

namespace TechAssessment.Application.BusinessLogic.Users.Validators
{
  public class AddEntryToPhoneBookCommandValidator : AbstractValidator<AddEntryToPhonebookCommand>
  {
    public AddEntryToPhoneBookCommandValidator()
    {
      RuleFor(x => x.Entry.Name).NotEmpty().WithMessage("Name is required")
          .MaximumLength(60).WithMessage("Maximum length for name is 60 chars")
          .MinimumLength(3).WithMessage("Minimum length for name is 3 chars");
      RuleFor(x => x.Entry.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
          .Matches("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$").WithMessage("Valid phone number is required")
          .MaximumLength(13).WithMessage("Maximum length for phone number is 13 chars")
          .MinimumLength(7).WithMessage("Minimum length for phone number is 7 chars");
    }
  }
}
