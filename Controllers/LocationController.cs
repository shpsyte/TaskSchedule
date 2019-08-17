using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskSchedule.Data;
using TaskSchedule.Domain;

namespace TaskSchedule.Controllers {

  [Authorize (Policy = "ADMIN")]
  public class LocationController : BaseController {

    public LocationController (UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<BaseController> logger, IUser currentUser) : base (userManager, context, logger, currentUser) { }

    [BindProperty]
    public Location Input { get; set; }
    public IActionResult List () {
      var data = _context.Location.ToListAsync ();
      return View (data.Result);
    }

    public IActionResult Add () {
      return View ();
    }

    [HttpPost]
    public async Task<IActionResult> Add (Location p) {
      if (ModelState.IsValid) {
        var location = new Location {
          Neighborhood = Input.Neighborhood,
          FundationName = Input.FundationName,
          Number = Input.Number,
          Phone = Input.Phone,
          PostalCode = Input.PostalCode,
          Responsible = Input.Responsible,
          Address = Input.Address,
          IsDeleted = false
        };

        await _context.Location.AddAsync (location);
        await _context.SaveChangesAsync ();
      }

      return RedirectToAction ("List", "Location");
    }

    public async Task<IActionResult> Edit (int id) {
      var location = await _context.Location.FindAsync (id);

      return View (location);

    }

    [HttpPost]
    public async Task<IActionResult> Edit (Location data, string submit) {

      data.IsDeleted = submit.Equals ("Delete");

      if (ModelState.IsValid) {
        _context.Location.Update (data);
        await _context.SaveChangesAsync ();
      }

      return RedirectToAction ("List", "Location");

    }
  }
}
