using System;
using System.Collections.Generic;
using AutoMapper;
using TechAssessment.Application.Interfaces.Mapping;
using TechAssessment.Domain;

namespace TechAssessment.Application.BusinessLogic.Users.Models
{
  public class UserViewModel : IHaveCustomMapping
  {

    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }

    public UserViewModel()
    {
    }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<UserViewModel, User>()
        .ReverseMap()
        .ForMember(m => m.Token, m => m.Ignore());
    }

  }
}
