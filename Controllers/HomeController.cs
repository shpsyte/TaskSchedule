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
    public HomeController (
      UserManager<ApplicationUser> userManager,
      ApplicationDbContext context,
      ILogger<BaseController> logger,
      IUser currentUser) : base (userManager, context, logger, currentUser) {
      this._taskServices = new TaskUserServices (context);

    }

    [BindProperty]
    public taskModel Input { get; set; }

    public IActionResult Index () {

      var filter = new TaskuserFilter (_isAdmin, _currentUser.Id ());
      var dataUser = new taskModel () {
        filter = filter,
        tasks = _taskServices.GetTask (filter)
      };

      LoadDataView ();
      return View (dataUser);
    }

    [HttpPost]
    public IActionResult Index (taskModel p) {

      p.filter.isAdmin = _isAdmin;
      p.filter.CurrentUserId = _currentUser.Id ();

      var dataUser = new taskModel () { filter = p.filter, tasks = _taskServices.GetTask (p.filter) };

      LoadDataView ();
      return View (dataUser);
    }

    private void LoadDataView () {
      ViewData["UserId"] = new SelectList (_userManager.Users.Where (a => a.Id == (_isAdmin ? a.Id : _currentUser.Id ())).ToList (), "Id", "Name");
      ViewData["LocationId"] = new SelectList (_context.Location.ToList (), "Id", "FundationName");
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
        if (!_isAdmin) {
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
        if (!_isAdmin) {
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
