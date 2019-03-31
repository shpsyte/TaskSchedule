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
  public class TaskUserController : BaseController {
    public TaskUserController (UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<BaseController> logger) : base (userManager, context, logger) { }

    [BindProperty]
    public TaskUser Input { get; set; }

    public IActionResult List () {
      return RedirectToAction ("index", "home");
    }
    public IActionResult Add () {
      ViewData["Time"] = new SelectList (TaskUser.TimeSpansInRange (TimeSpan.Parse ("00:00"), TimeSpan.Parse ("23:45"), TimeSpan.Parse ("00:15")));
      ViewData["UserId"] = new SelectList (_userManager.Users.ToList (), "Id", "Name");
      ViewData["LocationId"] = new SelectList (_context.Location.ToList (), "Id", "FundationName");
      return View ();
    }

    [HttpPost]
    public async Task<IActionResult> Add (TaskUser p) {
      if (ModelState.IsValid) {
        var task = new TaskUser {
          DateOfTest = Input.DateOfTest + Input.Time,
          DateOfEnd = null,
          Link = Input.Link,
          StudentId = Input.StudentId,
          StudentName = Input.StudentName,
          UserId = Input.UserId,
          LocationId = Input.LocationId
        };

        if (Input.LocationId.HasValue) {
          var LocationName = _context.Location.Find (Input.LocationId).FundationName;
          task.FundationName = LocationName;
        }

        var taskResult = await _context.TaskUser.AddAsync (task);
        var result = await _context.SaveChangesAsync ();
      }

      return RedirectToAction ("List", "TaskUser");
    }

  }
}
