using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TechAssessment.Application.Exceptions;
using TechAssessment.Domain;
using TechAssessment.Persistance;
using MediatR;

namespace TechAssessment.Application.BusinessLogic.Users.Commands
{
  public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
  {

      private readonly TechAssessmentDbContext _context;
      private readonly IMapper _mapper;

      public RegisterUserCommandHandler(TechAssessmentDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }


    public async  Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
      {
        var entity = _mapper.Map<User>(request.User);
      if (_context.Users.Any(u => u.Username.Equals(entity.Username)))
      {
        throw new DuplicateEntityException("User",entity.Username);
      } 

      _context.Users.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
      }
  }
}

