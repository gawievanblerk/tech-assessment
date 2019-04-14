using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using TechAssessment.Application.BusinessLogic.PhoneBooks.Models;
using TechAssessment.Application.Interfaces.Mapping;
using TechAssessment.Domain;

namespace TechAssessment.Application.BusinessLogic.PhoneBooks.Models
{
  public class PhoneBookListViewModel
  {

    public ICollection PhoneBooks { get; set; }

  }
}
