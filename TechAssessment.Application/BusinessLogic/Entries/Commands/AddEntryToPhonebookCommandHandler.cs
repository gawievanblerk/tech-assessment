using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TechAssessment.Persistance;
using MediatR;
using System.Linq;
using TechAssessment.Domain;
using TechAssessment.Application.Exceptions;

namespace TechAssessment.Application.BusinessLogic.Entries.Commands
{

  public class AddEntryToPhonebookCommandHandler : IRequestHandler<AddEntryToPhonebookCommand, int>
  {

    private readonly TechAssessmentDbContext _context;
    private readonly IMapper _mapper;

    public AddEntryToPhonebookCommandHandler(TechAssessmentDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public Task<int> HandleAsync(AddEntryToPhonebookCommand request, CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    public async Task<int> Handle(AddEntryToPhonebookCommand request, CancellationToken cancellationToken)
    {

      Entry entry = _mapper.Map<Entry>(request.Entry);

      PhoneBook phoneBook = _context.PhoneBooks.Where(p => p.Id == request.Entry.PhoneBookId).FirstOrDefault();
      if (phoneBook == null) {
        throw new NotFoundException(phoneBook.GetType().Name, request.Entry.PhoneBookId);
      }
      if (!phoneBook.Entries.Contains(entry))
      {
        phoneBook.Entries.Add(entry);
      } else {
        throw new DuplicateEntityException(entry.GetType().Name, entry.Name);
      }

      await _context.SaveChangesAsync();
      return entry.Id;

    }


  }
}