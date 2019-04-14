using System;
using System.Collections.Generic;
using System.Linq;
using TechAssessment.Domain;

namespace TechAssessment.Persistance
{
  public class TechAssessmentInitializer
  {

    private readonly Dictionary<int, User> Users = new Dictionary<int, User>();
    private readonly Dictionary<int, PhoneBook> PhoneBooks = new Dictionary<int, PhoneBook>();

    public static void Initialize(TechAssessmentDbContext context)
    {
      var initializer = new TechAssessmentInitializer();
      initializer.SeedEverything(context);
    }

    public void SeedEverything(TechAssessmentDbContext context)
    {
      context.Database.EnsureCreated();

      if (!context.Users.Any())
      {
        SeedUsers(context);
      }

      if (!context.PhoneBooks.Any())
      {
        SeedPhoneBooks(context);
      }


      return; // Db has been seeded

    }

    public void SeedUsers(TechAssessmentDbContext context)
    {
      var users = new[]
      {
        new User {Username = "admin", FirstName = "System", LastName = "Administrator" },
        new User {Username = "user", FirstName = "Test", LastName = "User"}
      };
      context.Users.AddRange(users);
      context.SaveChanges();
    }

    public void SeedPhoneBooks(TechAssessmentDbContext context)
    {
      var phoneBooks = new[]
      {
        new PhoneBook {Name = "Default" }
      };
      context.PhoneBooks.AddRange(phoneBooks);
      context.SaveChanges();
    }


  }

}
