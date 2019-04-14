using TechAssessment.Application.Infrastructure.Validation;
using TechAssessment.Application.Interfaces.Infrastructure.AutoMapper;
using TechAssessment.Persistance;
using TechAssessment.Presentation.Filters;
using TechAssessment.Application.BusinessLogic.Users.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechAssessment.Application.Helpers;
using NSwag.AspNetCore;
using TechAssessment.Application.BusinessLogic.PhoneBooks.Queries;

namespace TechAssessment.Presentation
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      // configure strongly typed settings objects
      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      // configure jwt authentication
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });

      // Add Cors Services
      services.AddCors(options =>
      {
        options.AddPolicy("AllowMyOrigin",
        builder => builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowCredentials()
          .AllowAnyHeader()
          );
      });

      services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
      services.AddMediatR(typeof(GetPhoneBooksListQueryHandler).GetTypeInfo().Assembly);

      services.AddDbContext<TechAssessmentDbContext>(options =>
        options.UseSqlite(Configuration.GetConnectionString("TechAssessmentDatabase")));

      services
          .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
          .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
          .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RehisterUserCommandValidator>());

      // Customise default API behavour
      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.SuppressModelStateInvalidFilter = true;
      });

      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });


      services.AddSwaggerDocument();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseAuthentication();

      app.UseCors("AllowMyOrigin");
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseSpaStaticFiles();

      app.UseSwaggerUi3(settings =>
      {
        settings.Path = "/api";
        settings.DocumentPath = "/api/specification.json";
      });

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";
        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }

      });
    }
  }
}
