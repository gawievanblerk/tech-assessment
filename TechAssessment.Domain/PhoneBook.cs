using System.Collections.Generic;

namespace TechAssessment.Domain
{
  public class PhoneBook
  {

    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Entry> Entries { get; private set; }

    public PhoneBook()
    {
      Entries = new List<Entry>();
    }

  }
}
