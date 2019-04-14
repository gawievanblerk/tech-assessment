using System;
using MediatR;
using TechAssessment.Application.BusinessLogic.Entries.Models;

namespace TechAssessment.Application.BusinessLogic.PhoneBooks.Queries
{
  public class GetPhoneBookEntriesQuery : IRequest<EntryListViewModel>
  {

    public int PhoneBookId { get; set; } 

    public GetPhoneBookEntriesQuery()
    {
    }

  }
}
