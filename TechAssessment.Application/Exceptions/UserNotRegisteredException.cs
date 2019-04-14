using System;

namespace TechAssessment.Application.Exceptions
{

  public class UserNotRegisteredException : Exception
  {
    public UserNotRegisteredException(string emailAddress)
        : base($"User \"{emailAddress}\" not registered.")
    {
    }

  }

}

