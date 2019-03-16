using Microsoft.AspNetCore.Identity;
using TaskSchedule.Domain;

namespace TaskSchedule.Data {
  public class DBInitializer {

    static string email = "admin@admin.com";
    static string password = "Task@2019";
    static string roleAdm = "ADMINISTRATOR";
    static string roleUser = "SUPERVISOR";

    public static void SeedData (UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
      SeedRoles (roleManager);
      SeedUsers (userManager);
    }
    private static void SeedRoles (RoleManager<IdentityRole> roleManager) {
      if (!roleManager.RoleExistsAsync (roleAdm).Result) {
        IdentityRole role = new IdentityRole ();
        role.Name = roleAdm;
        IdentityResult roleResult = roleManager.
        CreateAsync (role).Result;
      }
      if (!roleManager.RoleExistsAsync (roleUser).Result) {
        IdentityRole role = new IdentityRole ();
        role.Name = roleUser;
        IdentityResult roleResult = roleManager.
        CreateAsync (role).Result;
      }

    }
    private static void SeedUsers (UserManager<IdentityUser> userManager) {
      if (userManager.FindByEmailAsync (email).Result == null) {
        IdentityUser user = new IdentityUser {
        UserName = email,
        Email = email
        };
        IdentityResult result = userManager.CreateAsync (user, password).Result;
        if (result.Succeeded) {
          userManager.AddToRoleAsync (user, roleAdm).Wait ();
        }
      }
    }

  }
}
