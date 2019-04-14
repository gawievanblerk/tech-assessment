using System;
namespace TechAssessment.Application.Exceptions
{

    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException(string name, object key)
            : base($"Duplicate Entity \"{name}\" ({key}).")
        {
        }
    }

}

