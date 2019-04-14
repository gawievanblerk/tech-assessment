using AutoMapper;
using TechAssessment.Application.Interfaces.Mapping;
using TechAssessment.Domain;

namespace TechAssessment.Application.BusinessLogic.Entries.Models
{
  public class EntryViewModel : IHaveCustomMapping
  {

    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public int PhoneBookId { get; set; }

    public EntryViewModel()
    {
    }

    public void CreateMappings(Profile configuration)
    {
      configuration.CreateMap<EntryViewModel, Entry>()
        .ReverseMap()
        .ForMember(m => m.PhoneBookId, m => m.Ignore());
    }

  }
}
