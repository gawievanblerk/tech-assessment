using Microsoft.EntityFrameworkCore;
using TechAssessment.Domain;
using System.Reflection;
using System.Linq;
using System;

namespace TechAssessment.Persistance
{
  public class TechAssessmentDbContext : DbContext
  {
    public TechAssessmentDbContext(DbContextOptions<TechAssessmentDbContext> options)
            : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<PhoneBook> PhoneBooks { get; set; }
    public DbSet<Entry> Entries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<PhoneBook>()
          .HasKey(b => b.Id);
      modelBuilder.Entity<Entry>()
          .HasKey(e => e.Id);

      var configurations = Assembly.GetExecutingAssembly().DefinedTypes.Where(t =>
                t.ImplementedInterfaces.Any(i =>
                   i.IsGenericType &&
                   i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name,
                          StringComparison.InvariantCultureIgnoreCase)
                 ) &&
                 t.IsClass &&
                 !t.IsAbstract &&
                 !t.IsNested)
                 .ToList();

      foreach (var configuration in configurations)
      {
        var entityType = configuration.BaseType.GenericTypeArguments.SingleOrDefault(t => t.IsClass);

        var applyConfigMethods = typeof(ModelBuilder).GetMethods().Where(m => m.Name.Equals("ApplyConfiguration") && m.IsGenericMethod);
        var applyConfigMethod = applyConfigMethods.Single(m => m.GetParameters().Any(p => p.ParameterType.Name.Equals(typeof(IEntityTypeConfiguration<>).Name)));

        var applyConfigGenericMethod = applyConfigMethod.MakeGenericMethod(entityType);

        applyConfigGenericMethod.Invoke(modelBuilder,
                     new object[] { Activator.CreateInstance(configuration) });
      }

    }

  }
}
