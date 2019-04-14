using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TechAssessment.Application.Exceptions;
using TechAssessment.Application.BusinessLogic.Users.Models;
using TechAssessment.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using TechAssessment.Application.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace TechAssessment.Application.BusinessLogic.Users.Queries
{
  public class CheckUserRegistrationQueryHandler : IRequestHandler<CheckUserRegistrationQuery, UserViewModel>
  {

    private readonly TechAssessmentDbContext _context;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public CheckUserRegistrationQueryHandler(TechAssessmentDbContext context, IMapper mapper, IOptions<AppSettings> appSettings)
    {
      _context = context;
      _mapper = mapper;
      _appSettings = appSettings.Value;
    }


    public async Task<UserViewModel> Handle(CheckUserRegistrationQuery request, CancellationToken cancellationToken)
    {

      var user = _mapper.Map<UserViewModel>(await _context
          .Users.Where(u => u.Username.Equals(request.Username))
          .SingleOrDefaultAsync(cancellationToken));

      if (user == null)
      {
        throw new UserNotRegisteredException(request.Username);
      }

      // authentication successful so generate jwt token
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Token = tokenHandler.WriteToken(token);

      return user;

    }

  }
}
