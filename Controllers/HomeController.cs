using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskSchedule.Data;
using TaskSchedule.Domain;
using TaskSchedule.Models;

namespace TaskSchedule.Controllers {
  public class HomeController : BaseController {

    private TaskUserServices _taskServices;
    public HomeController (UserManager<ApplicationUser> userManager,
      ApplicationDbContext context,
      ILogger<BaseController> logger) : base (userManager, context, logger) {
      this._taskServices = new TaskUserServices (context);
    }

    [BindProperty]
    public taskModel Input { get; set; }

    public async Task<IActionResult> Index () {
      var filter = new TaskuserFilter (User.IsInRole ("ADMINISTRATOR"));
      var dataUser = new taskModel () { filter = filter, tasks = await _taskServices.GetTaskAsync (filter) };
      LoadDataView ();
      return View (dataUser);
    }

    [HttpPost]
    public async Task<IActionResult> Index (taskModel p) {
      LoadDataView ();
      p.filter.isAdmin = User.IsInRole ("ADMINISTRATOR");
      var dataUser = new taskModel () { filter = p.filter, tasks = await _taskServices.GetTaskAsync (p.filter) };
      return View (dataUser);
    }

    private void LoadDataView () {
      ViewData["UserId"] = new SelectList (_userManager.Users.ToList (), "Id", "Name");
      ViewData["LocationId"] = new SelectList (_context.Location.ToList (), "Id", "FundationName");
    }

    private async Task<ApplicationUser> GetCurrentUser () {
      return await _userManager.GetUserAsync (HttpContext.User);
    }

    public IActionResult Privacy () {
      return View ();
    }

    [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error () {
      return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Details (int id) {
      TaskUser task = await _context.TaskUser
        .Where (a => a.Id == id)
        .Include (a => a.User)
        .Include (l => l.Location)
        .FirstAsync ();

      if (task.Location == null) {
        task.LocationId = _context.Location.FirstOrDefault ().Id;
        _context.Update (task);
        _context.SaveChanges ();
      }

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
      task.DateOfEnd = System.DateTime.Now;
      _context.Entry (task).State = EntityState.Modified;
      _context.SaveChanges ();

      return RedirectToAction ("index", "Home");
    }

  }
}
