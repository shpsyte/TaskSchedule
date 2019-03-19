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
    public LocationController (UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<BaseController> logger) : base (userManager, context, logger) { }

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
          Address = Input.Address
        };

        await _context.Location.AddAsync (location);
        await _context.SaveChangesAsync ();
      }

      return RedirectToAction ("List", "Location");
    }

  }
}
