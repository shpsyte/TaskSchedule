using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskSchedule.Data;
using TaskSchedule.Domain;
using TaskSchedule.Models;

namespace TaskSchedule.Controllers {
  [Authorize (Policy = "ADMIN")]
  public class UserController : BaseController {

    public UserController (UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<UserController> logger, IUser currentUser) : base (userManager, context, logger, currentUser) { }

    [BindProperty]
    public UserModel Input { get; set; }

    public IActionResult List () {
      var users = _userManager.Users.Where (a => a.EmailConfirmed == false).ToList ();
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

    public async Task<ActionResult> SetPassWord (string email) {
      var user = await _userManager.FindByNameAsync (email);

      if (user == null) {
        return RedirectToAction ("List", "User");
      }

      var data = new UserModel () {
        Name = user.Name,
        Email = email
      };

      return View (data);

    }

    [HttpPost]
    public async Task<ActionResult> SetPassWord (UserModel p) {
      var user = await _userManager.FindByNameAsync (p.Email);

      if (user == null) {
        return RedirectToAction ("index", "home");
      }

      var data = new UserModel () {
        Name = user.Name,
        Email = p.Email
      };

      if (ModelState.IsValid) {
        var newPassword = _userManager.PasswordHasher.HashPassword (user, p.Password);

        user.PasswordHash = newPassword;
        var res = await _userManager.UpdateAsync (user);

        if (res.Succeeded) {
          _logger.LogInformation ("User created a new account with password.");
        } else {
          foreach (var error in res.Errors) {
            ModelState.AddModelError (string.Empty, error.Description);
          }
          return View (data);
        }

      }

      // compute the new hash string

      return RedirectToAction ("List", "User");

    }

    [HttpPost]
    public async Task<ActionResult> DeleteUser (UserModel p) {
      var user = await _userManager.FindByNameAsync (p.Email);

      if (user == null) {
        return RedirectToAction ("index", "home");
      }

      user.EmailConfirmed = true;
      var res = await _userManager.UpdateAsync (user);

      if (res.Succeeded) {
        _logger.LogInformation ("User removed.");
      } else {
        foreach (var error in res.Errors) {
          ModelState.AddModelError (string.Empty, error.Description);
        }
        return RedirectToAction ("SetPassWord", "User", new { email = p.Email });

      }

      return RedirectToAction ("List", "User");
    }

  }
}
