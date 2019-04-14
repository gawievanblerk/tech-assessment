using System;
using TechAssessment.Application.BusinessLogic.PhoneBooks.Models;
using MediatR;


namespace TechAssessment.Application.UseCases.PhoneBooks.Queries
{
  public class GetPhoneBooksListQuery : IRequest<PhoneBookListViewModel>
    {
        public GetPhoneBooksListQuery()
        {
        }
    }
}
