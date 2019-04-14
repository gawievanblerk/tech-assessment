using System;
using AutoMapper;
using TechAssessment.Application.Interfaces.Mapping;
using TechAssessment.Domain;

namespace TechAssessment.Application.BusinessLogic.PhoneBooks.Models
{
  public class PhoneBookViewModel : IHaveCustomMapping
  {

    public int Id { get; set; }
    public string Name { get; set; }
  
    public PhoneBookViewModel() 
    {
    }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<PhoneBookViewModel, PhoneBook>();
    }

  }
}
