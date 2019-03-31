using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskSchedule.Domain;

namespace TaskSchedule.Data {
  public class DBInitializer {

    static string email = "admin@admin.com";
    static string password = "Task@2019";
    static string roleAdm = "ADMINISTRATOR";
    static string roleUser = "SUPERVISOR";

    public static void SeedData (ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) {
      MigrateDB (context);
      SeedRoles (roleManager);
      SeedUsers (userManager);
      SeedLocation (context);
    }

    private static void MigrateDB (ApplicationDbContext context) {
      context.Database.Migrate ();
    }

    private static void SeedRoles (RoleManager<ApplicationRole> roleManager) {

      if (!roleManager.RoleExistsAsync (roleAdm).Result) {
        ApplicationRole role = new ApplicationRole ();
        role.Name = roleAdm;
        IdentityResult roleResult = roleManager.
        CreateAsync (role).Result;
      }

      if (!roleManager.RoleExistsAsync (roleUser).Result) {
        ApplicationRole role = new ApplicationRole ();
        role.Name = roleUser;
        IdentityResult roleResult = roleManager.
        CreateAsync (role).Result;
      }

    }
    private static void SeedUsers (UserManager<ApplicationUser> userManager) {
      if (userManager.FindByEmailAsync (email).Result == null) {
        ApplicationUser user = new ApplicationUser {
        Name = email,
        UserName = email,
        Email = email
        };
        IdentityResult result = userManager.CreateAsync (user, password).Result;
        if (result.Succeeded) {
          userManager.AddToRoleAsync (user, roleAdm).Wait ();
        }
      }
    }

    private static void SeedLocation (ApplicationDbContext context) {
      if (context.Location.Count () == 0) {
        var location = new Location () {
        Address = "TDCO", CityAndState = "Cwb/PR", FundationName = "TDCO", Neighborhood = "TDCO", Number = "0", Phone = "41", PostalCode = "TDCO", Responsible = "TDCO"
        };

        context.Location.Add (location);
        context.SaveChanges ();
      };

    }
  }

}
