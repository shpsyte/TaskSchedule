using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskSchedule.Data;
using TaskSchedule.Domain;
using TaskSchedule.Models;

namespace TaskSchedule.Controllers {
  public class UserController : BaseController {

    public UserController (UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<UserController> logger) : base (userManager, context, logger) { }

    [BindProperty]
    public UserModel Input { get; set; }

    public IActionResult List () {
      var users = _userManager.Users.ToList ();

      return View (users);
    }

    public IActionResult Add () {
      return View (new UserModel ());
    }

    [HttpPost]
    public async Task<IActionResult> Add (UserModel p) {
      // first add User

      if (ModelState.IsValid) {
        var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Name = Input.Name };
        var result = await _userManager.CreateAsync (user, Input.Password);
        if (result.Succeeded) {

          _logger.LogInformation ("User created a new account with password.");
          _userManager.AddToRoleAsync (user, "SUPERVISOR").Wait ();
        }

        foreach (var error in result.Errors) {
          ModelState.AddModelError (string.Empty, error.Description);
        }

      }
      return RedirectToAction ("List", "User");
    }

  }
}
