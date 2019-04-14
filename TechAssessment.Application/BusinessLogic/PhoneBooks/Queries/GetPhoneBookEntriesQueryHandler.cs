using System;
using System.Threading;
using AutoMapper;
using MediatR;
using TechAssessment.Application.BusinessLogic.Entries.Models;
using TechAssessment.Persistance;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TechAssessment.Application.Exceptions;

namespace TechAssessment.Application.BusinessLogic.PhoneBooks.Queries
{
  public class GetPhoneBookEntriesQueryHandler : IRequestHandler<GetPhoneBookEntriesQuery, EntryListViewModel>
  {
    private readonly TechAssessmentDbContext _context;
    private readonly IMapper _mapper;

    public GetPhoneBookEntriesQueryHandler(TechAssessmentDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async System.Threading.Tasks.Task<EntryListViewModel> Handle(GetPhoneBookEntriesQuery request, CancellationToken cancellationToken)
    {

      var phoneBook = await _context.PhoneBooks.Include(p => p.Entries).FirstOrDefaultAsync(p => p.Id == request.PhoneBookId);
      if (phoneBook != null)
      {
        var model = new EntryListViewModel
        {
          Entries = _mapper.Map<List<EntryViewModel>>(phoneBook.Entries)
        };
        return model;
      } else {
        throw new NotFoundException(phoneBook.GetType().Name, request.PhoneBookId);
      }
    }
  }
}
