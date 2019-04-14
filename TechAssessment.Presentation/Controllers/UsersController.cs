using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechAssessment.Application.BusinessLogic.Users.Commands;
using System.Net;
using TechAssessment.Application.BusinessLogic.Users.Queries;
using TechAssessment.Application.BusinessLogic.Users.Models;
using NSwag.Annotations;
using System.Data;
using TechAssessment.Application.Exceptions;
using System;
using Microsoft.AspNetCore.Authorization;

namespace TechAssessment.Presentation.Controllers
{
  public class UsersController : BaseController
  {

    // GET: api/users/CheckUserRegistration
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<UserViewModel>> CheckUserRegistration([FromQuery] CheckUserRegistrationQuery query)
    {
      return Ok(await Mediator.Send(query));
    }

    // POST api/users
    [HttpPost, AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
    {
      await Mediator.Send(command);
      return NoContent();
    }

  }
}
