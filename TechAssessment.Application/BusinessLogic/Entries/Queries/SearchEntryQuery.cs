using MediatR;
using TechAssessment.Application.BusinessLogic.Entries.Models;


namespace TechAssessment.Application.BusinessLogic.Entries.Queries
{
  public class SearchEntryQuery : IRequest<EntryListViewModel>
  {

    public string  SearchString { get; set; }

    public SearchEntryQuery()
    {
    }
  }
}
