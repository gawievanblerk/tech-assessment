using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace TechAssessment.Presentation.Controllers
{
  [ApiController]
  [Authorize]
  [Route("api/[controller]/[action]")]
  public abstract class BaseController : Controller
  {
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
  }
}
