using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TechAssessment.Application.BusinessLogic.Users.Models;
using TechAssessment.Domain;
using TechAssessment.Persistance;
using MediatR;

namespace TechAssessment.Application.BusinessLogic.Users.Commands
{

    public class RegisterUserCommand : IRequest<int>
    {
       
       public UserViewModel User { get; set; }

    }

}