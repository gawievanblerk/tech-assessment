using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using TechAssessment.Application.BusinessLogic.PhoneBooks.Models;
using TechAssessment.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TechAssessment.Application.BusinessLogic.Entries.Models;


namespace TechAssessment.Application.BusinessLogic.Entries.Queries
{
  public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, EntryListViewModel>
  {

    private readonly TechAssessmentDbContext _context;
    private readonly IMapper _mapper;


    public SearchEntryQueryHandler(TechAssessmentDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async  System.Threading.Tasks.Task<EntryListViewModel> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
    {
      var entries = await _context.Entries.Where(e => e.Name.ToLower().Contains(request.SearchString.ToLower())).ToListAsync();
      var model = new EntryListViewModel
      {
        Entries = _mapper.Map<List<EntryViewModel>>(entries)
      };
      return model;
    }

  }
}
