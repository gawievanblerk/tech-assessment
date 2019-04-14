using System;
using TechAssessment.Persistance.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace TechAssessment.Persistance
{
    public class TechAssessmentDbContextFactory : DesignTimeDbContextFactoryBase<TechAssessmentDbContext>
    {

        protected override TechAssessmentDbContext CreateNewInstance(DbContextOptions<TechAssessmentDbContext> options)
        {
            return new TechAssessmentDbContext(options);
        }
    }
}
