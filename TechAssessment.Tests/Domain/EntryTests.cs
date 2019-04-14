using System;
using TechAssessment.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Domain
{
  [TestClass]
  public class EntryTests
  {
    [TestMethod]
    public void CanCreateEntry()
    {
      Entry entry = new Entry();
      Assert.IsInstanceOfType(entry, typeof(Entry));
    }

    [TestMethod]
    public void CanSetEntryName()
    {
      Entry entry = new Entry();
      entry.Name = "ABC123";
      Assert.AreEqual("ABC123", entry.Name);
    }

    [TestMethod]
    public void CanSetEntryPhoneNumber()
    {
      Entry entry = new Entry();
      entry.PhoneNumber = "0825551234";
      Assert.AreEqual("0825551234", entry.PhoneNumber);
    }


  }
}
