using MediatR;
using TechAssessment.Application.BusinessLogic.Entries.Models;

namespace TechAssessment.Application.BusinessLogic.Entries.Commands
{

  public class AddEntryToPhonebookCommand : IRequest<int>
  {

    public EntryViewModel Entry { get; set; }

  }

}
