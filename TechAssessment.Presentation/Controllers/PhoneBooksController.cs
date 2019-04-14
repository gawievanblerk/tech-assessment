using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechAssessment.Application.BusinessLogic.PhoneBooks.Models;
using TechAssessment.Application.UseCases.PhoneBooks.Queries;
using TechAssessment.Application.BusinessLogic.PhoneBooks.Queries;
using TechAssessment.Application.BusinessLogic.Entries.Models;
using TechAssessment.Application.BusinessLogic.Entries.Commands;

namespace TechAssessment.Presentation.Controllers
{

  public class PhoneBooksController : BaseController
  {

    // GET /api/PhoneBooks/GetAllPhoneBooks
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<PhoneBookListViewModel>> GetAllPhoneBooks()
    {
      var query = new GetPhoneBooksListQuery();
      return Ok(await Mediator.Send(query));
    }

    // GET api/PhoneBooks/GetPhoneBookEntries
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<EntryListViewModel>> GetPhoneBookEntries([FromQuery] GetPhoneBookEntriesQuery query)
    {
      return Ok(await Mediator.Send(query));
    }

    // POST /api/PhoneBooks/AddPhoneBookEntry
    [HttpPost, AllowAnonymous]
    public async Task<ActionResult<int>> AddEntry([FromBody] AddEntryToPhonebookCommand command) {
      var entryId = await Mediator.Send(command);
      return Ok(entryId);
    }

  }

}
