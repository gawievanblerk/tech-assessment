using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechAssessment.Domain;

namespace Tests.Domain
{
  [TestClass]
  public class PhoneBookTests
  {
    [TestMethod]
    public void CanCreatePhoneBook()
    {
      PhoneBook phoneBook = new PhoneBook();
      Assert.IsInstanceOfType(phoneBook, typeof(PhoneBook));
    }

    [TestMethod]
    public void CanSetName()
    {
      PhoneBook phoneBook = new PhoneBook();
      phoneBook.Name = "ABC123";
      Assert.AreEqual("ABC123", phoneBook.Name);
    }

  }
}
