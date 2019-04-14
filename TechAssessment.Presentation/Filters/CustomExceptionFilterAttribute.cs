using System;
using System.Net;
using TechAssessment.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechAssessment.Presentation.Filters
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
  {
    public override void OnException(ExceptionContext context)
    {

      var code = HttpStatusCode.InternalServerError;

      if (context.Exception is ValidationException)
      {
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new JsonResult(
            ((ValidationException)context.Exception).Failures);
        return;
      } 
      else if (context.Exception is NotFoundException)
      {
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        context.Result = new JsonResult(
            ((NotFoundException)context.Exception).Message);
        return;
      }  
      else if (context.Exception is DuplicateEntityException)
      {
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        context.Result = new JsonResult(
            ((DuplicateEntityException)context.Exception).Message);
        return;
      }

      context.HttpContext.Response.ContentType = "application/json";
      context.HttpContext.Response.StatusCode = (int)code;
      context.Result = new JsonResult(new
      {
        error = new[] { context.Exception.Message },
        stackTrace = context.Exception.StackTrace
      });
    }
  }
}