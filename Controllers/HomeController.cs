using System;
using System.Collections.Generic;
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
  public class HomeController : BaseController {

    public HomeController (UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<BaseController> logger) : base (userManager, context, logger) { }

    public async Task<IActionResult> Index () {
      var user = await _userManager.GetUserAsync (HttpContext.User);
      var dataUser = _context.TaskUser.Include (a => a.User).AsNoTracking ().AsQueryable ();

      if (!User.IsInRole ("ADMINISTRATOR")) {
        dataUser = dataUser.Where (a => a.UserId == user.Id);
      }

      //dataUser = dataUser.Where (a => a.Done == false);
      return View (dataUser);
    }

    public IActionResult Privacy () {
      return View ();
    }

    [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error () {
      return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Details (int id) {
      TaskUser task = await _context.TaskUser.Where (a => a.Id == id).Include (a => a.User).FirstAsync ();
      var user = await _userManager.GetUserAsync (HttpContext.User);

      if (task.UserId != user.Id) {
        if (!User.IsInRole ("ADMINISTRATOR")) {
          return RedirectToAction ("Error");
        }
      }

      return View (task);
    }

    public async Task<IActionResult> Done (int id, TaskUser p) {
      TaskUser task = await _context.TaskUser.FindAsync (id);

      if (task.Done)
        return RedirectToAction ("index", "Home");

      var user = await _userManager.GetUserAsync (HttpContext.User);

      if (task.UserId != user.Id) {
        if (!User.IsInRole ("ADMINISTRATOR")) {
          return RedirectToAction ("Error");
        }
      }

      task.Done = true;
      _context.Entry (task).State = EntityState.Modified;
      _context.SaveChanges ();

      return RedirectToAction ("index", "Home");
    }

  }
}
