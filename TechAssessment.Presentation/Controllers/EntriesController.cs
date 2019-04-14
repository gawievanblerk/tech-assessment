using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using TechAssessment.Application.BusinessLogic.Entries.Commands;
using Microsoft.AspNetCore.Authorization;
using TechAssessment.Application.BusinessLogic.Entries.Queries;
using TechAssessment.Application.BusinessLogic.Entries.Models;

namespace TechAssessment.Presentation.Controllers
{
  public class EntriesController : BaseController
  {

    // POST api/AddEntryToPhonebook
    [HttpPost, AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> AddEntryToPhonebook(AddEntryToPhonebookCommand command)
    {
      return Ok( await Mediator.Send(command));
    }

    // POST api/SearchPhonebookEntry
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<EntryListViewModel>> SearchPhonebookEntry([FromQuery] SearchEntryQuery query)
    {
      return Ok(await Mediator.Send(query));
    }

  }
}
