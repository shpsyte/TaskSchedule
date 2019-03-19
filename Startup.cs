using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskSchedule.Data;
using TaskSchedule.Domain;

namespace TaskSchedule {
  public class Startup {
    public Startup (IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices (IServiceCollection services) {
      services.Configure<CookiePolicyOptions> (options => {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddDbContext<ApplicationDbContext> (options =>
        options.UseMySql (
          Configuration.GetConnectionString ("DefaultConnection")));

      services.AddIdentity<ApplicationUser, ApplicationRole> ()
        .AddEntityFrameworkStores<ApplicationDbContext> ()
        .AddDefaultUI (UIFramework.Bootstrap4)
        .AddDefaultTokenProviders ();

      services.Configure<RequestLocalizationOptions> (options => {
        var supportedCultures = new [] {
        new CultureInfo ("pt-BR")
        };
        options.DefaultRequestCulture = new RequestCulture (culture: "pt-BR", uiCulture: "pt-BR");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;

      });

      services.AddAuthorization (options => {
        options.AddPolicy ("ADMIN", policy => policy.RequireRole ("ADMINISTRATOR"));
      });

      services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure (IApplicationBuilder app,
      IHostingEnvironment env,
      UserManager<ApplicationUser> userManager,
      RoleManager<ApplicationRole> roleManager,
      ApplicationDbContext context) {
      if (env.IsDevelopment ()) {
        app.UseDeveloperExceptionPage ();
        app.UseDatabaseErrorPage ();
      } else {
        app.UseExceptionHandler ("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts ();
      }

      app.UseHttpsRedirection ();
      app.UseStaticFiles ();
      app.UseCookiePolicy ();

      app.UseAuthentication ();

      DBInitializer.SeedData (context, userManager, roleManager);

      app.UseMvc (routes => {
        routes.MapRoute (
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
