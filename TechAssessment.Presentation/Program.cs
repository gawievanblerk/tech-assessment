using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechAssessment.Persistance;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TechAssessment.Presentation
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateWebHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        try
        {
          var context = scope.ServiceProvider.GetService<TechAssessmentDbContext>();
          context.Database.Migrate();
          TechAssessmentInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
          var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred while migrating or initializing the database.");
        }
      }

      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        new WebHostBuilder()
            .UseKestrel()
            .UseUrls("https://localhost:5001/")
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
              var env = hostingContext.HostingEnvironment;
              config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
              config.AddEnvironmentVariables();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
              logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
              logging.AddConsole();
              logging.AddDebug();
            })
            .UseStartup<Startup>();
  }
}
