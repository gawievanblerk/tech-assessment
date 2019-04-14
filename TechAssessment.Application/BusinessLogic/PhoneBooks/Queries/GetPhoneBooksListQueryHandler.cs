using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TechAssessment.Application.BusinessLogic.PhoneBooks.Models;
using TechAssessment.Application.UseCases.PhoneBooks.Queries;
using TechAssessment.Persistance;
using MediatR;
using SQLitePCL;
using System.Linq;
using TechAssessment.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TechAssessment.Application.BusinessLogic.PhoneBooks.Queries
{
  public class GetPhoneBooksListQueryHandler : IRequestHandler<GetPhoneBooksListQuery, PhoneBookListViewModel>
  {

    private readonly TechAssessmentDbContext _context;
    private readonly IMapper _mapper;

    public GetPhoneBooksListQueryHandler(TechAssessmentDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<PhoneBookListViewModel> Handle(GetPhoneBooksListQuery request, CancellationToken cancellationToken)
    {
      var phoneBooks = await _context.PhoneBooks.OrderBy(p => p.Name).ToListAsync();
      var model = new PhoneBookListViewModel
      {
        PhoneBooks = _mapper.Map<List<PhoneBookViewModel>>(phoneBooks)
      };

      return model;

    }


  }
}
