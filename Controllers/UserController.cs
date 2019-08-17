using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskSchedule.Data;
using TaskSchedule.Domain;
using TaskSchedule.Models;

namespace TaskSchedule.Controllers {
  [Authorize (Policy = "ADMIN")]
  public class UserController : BaseController {

    public RoleManager<ApplicationRole> _roles;

    public UserController (
      UserManager<ApplicationUser> userManager,
      ApplicationDbContext context,
      ILogger<UserController> logger, IUser currentUser, RoleManager<ApplicationRole> roles) : base (userManager, context, logger, currentUser) {
      this._roles = roles;
    }

    [BindProperty]
    public UserModel Input { get; set; }

    public IActionResult List () {
      var users = _userManager.Users.ToList ();

      users.ForEach (async user => {
        var userRoles = await _userManager.GetRolesAsync (user);
        user.IsAdmin = userRoles.Contains ("ADMINISTRATOR");
      });
      return View (users);
    }

    public IActionResult Add () {
      ViewData["RoleID"] = new SelectList (_roles.Roles.ToList (), "Id", "Name");

      return View (new UserModel ());
    }

    [HttpPost]
    public async Task<IActionResult> Add (UserModel p) {
      // first add User
      ViewData["RoleID"] = new SelectList (_roles.Roles.ToList (), "Id", "Name");
      var _name = await _roles.FindByIdAsync (p.RoleID.ToString ());

      if (ModelState.IsValid) {
        var user = new ApplicationUser {
          UserName = Input.Email,
          Email = Input.Email,
          Name = Input.Name,
          PhoneNumber = Input.PhoneNumber,
          PasswordTip = Input.PasswordTip
        };

        var result = await _userManager.CreateAsync (user, Input.Password);
        if (result.Succeeded) {

          _logger.LogInformation ("User created a new account with password.");
          _userManager.AddToRoleAsync (user, _name.Name.ToUpper ()).Wait ();
          // _userManager.AddToRoleAsync (user, "SUPERVISOR").Wait ();
        }

        foreach (var error in result.Errors) {
          ModelState.AddModelError ("Email", error.Description);
          return View (p);
        }

      }
      return RedirectToAction ("List", "User");
    }

    public async Task<ActionResult> SetPassWord (string email) {
      var user = await _userManager.FindByNameAsync (email);

      if (user == null) {
        return RedirectToAction ("List", "User");
      }

      var userRoles = await _userManager.GetRolesAsync (user);
      user.IsAdmin = userRoles.Contains ("ADMINISTRATOR");

      ViewData["RoleID"] = new SelectList (_roles.Roles.ToList (), "Id", "Name", user.IsAdmin ? 1 : 2);

      var data = new UserModel () {
        Name = user.Name,
        Email = email,
        PasswordTip = user.PasswordTip,
        EmailConfirmado = user.EmailConfirmed,
        RoleID = user.IsAdmin ? 1 : 2
      };

      return View (data);

    }

    [HttpPost]
    public async Task<ActionResult> SetPassWord (UserModel p) {

      var user = await _userManager.FindByNameAsync (p.Email);

      if (user == null) {
        return RedirectToAction ("index", "home");
      }

      ViewData["RoleID"] = new SelectList (_roles.Roles.ToList (), "Id", "Name", p.RoleID);

      var data = new UserModel () {
        Name = user.Name,
        Email = p.Email
      };

      if (ModelState.IsValid) {
        var newPassword = _userManager.PasswordHasher.HashPassword (user, p.Password);

        user.PasswordHash = newPassword;
        user.EmailConfirmed = false;
        var res = await _userManager.UpdateAsync (user);

        if (res.Succeeded) {
          _logger.LogInformation ("User created a new account with password.");

          var _name = await _roles.FindByIdAsync (p.RoleID.ToString ());
          var roles = _roles.Roles.ToList ();

          //retirar das roles
          roles.ForEach (role => {
            _userManager.RemoveFromRoleAsync (user, role.Name.ToUpper ()).Wait ();
          });

          // adicionar na roel selecioanda
          _userManager.AddToRoleAsync (user, _name.Name.ToUpper ()).Wait ();
          // _userManager.AddToRoleAsync (user, "SUPERVISOR").Wait ();

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
