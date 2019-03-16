using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskSchedule.Data;
using TaskSchedule.Domain;
using TaskSchedule.Models;

namespace TaskSchedule.Controllers {
  public class AddUserController : BaseController {

    public AddUserController (UserManager<IdentityUser> userManager, ApplicationDbContext context, ILogger<AddUserController> logger) : base (userManager, context, logger) { }

    [BindProperty]
    public UserModel Input { get; set; }

    public IActionResult Create () {
      return View (new UserModel ());
    }

    [HttpPost]
    public async Task<IActionResult> Create (UserModel p) {
      // first add User

      if (ModelState.IsValid) {
        var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
        var result = await _userManager.CreateAsync (user, Input.Password);
        if (result.Succeeded) {
          UserSetting userSetting = new UserSetting () { Name = Input.Name, User = user };
          var userresult = await _context.UserSetting.AddAsync (userSetting);
          var dbrestul = await _context.SaveChangesAsync ();
          _logger.LogInformation ("User created a new account with password.");
          _userManager.AddToRoleAsync (user, "SUPERVISOR").Wait ();
        }

        foreach (var error in result.Errors) {
          ModelState.AddModelError (string.Empty, error.Description);
        }

      }
      return RedirectToAction ("Index", "Home");
    }

  }
}
