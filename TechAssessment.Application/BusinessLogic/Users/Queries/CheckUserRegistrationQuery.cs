using System;
using System.Threading;
using System.Threading.Tasks;
using TechAssessment.Application.BusinessLogic.Users.Models;
using TechAssessment.Persistance;
using MediatR;
using System.Linq;
using AutoMapper;
using TechAssessment.Application.Exceptions;
using TechAssessment.Domain;
using Microsoft.EntityFrameworkCore;

namespace TechAssessment.Application.BusinessLogic.Users.Queries
{
    public class CheckUserRegistrationQuery : IRequest<UserViewModel>
    {

        public string Username { get; set; }

        public CheckUserRegistrationQuery()
        {
        }

    }

}
