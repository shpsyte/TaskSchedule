using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
  public class TaskUserController : BaseController {
    public TaskUserController (UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<BaseController> logger) : base (userManager, context, logger) { }

    [BindProperty]
    public TaskUser Input { get; set; }

    public IActionResult List () {
      var data = _context.TaskUser.Include (a => a.User).ToList ();
      return View (data);
    }
    public IActionResult Add () {
      ViewData["Time"] = new SelectList (TaskUser.TimeSpansInRange (TimeSpan.Parse ("00:00"), TimeSpan.Parse ("23:45"), TimeSpan.Parse ("00:15")));
      ViewData["UserId"] = new SelectList (_userManager.Users.ToList (), "Id", "Name");
      return View ();
    }

    [HttpPost]
    public async Task<IActionResult> Add (TaskUser p) {
      if (ModelState.IsValid) {
        var task = new TaskUser {
          DateOfTest = Input.DateOfTest,
          DateOfEnd = null,
          FundationName = Input.FundationName,
          Link = Input.Link,
          StudentId = Input.StudentId,
          StudentName = Input.StudentName,
          UserId = Input.UserId,
        };

        var taskResult = await _context.TaskUser.AddAsync (task);
        var result = await _context.SaveChangesAsync ();
      }

      return RedirectToAction ("List", "TaskUser");
    }

  }
}
