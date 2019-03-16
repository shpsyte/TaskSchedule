using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskSchedule.Data;
using TaskSchedule.Models;

namespace TaskSchedule.Controllers {
  public class BaseController : Controller {

    public ApplicationDbContext _context;
    public ILogger<AddUserController> _logger;
    public UserManager<IdentityUser> _userManager;

    public BaseController (UserManager<IdentityUser> userManager, ApplicationDbContext context, ILogger<AddUserController> logger) {
      _context = context;
      _logger = logger;
      _userManager = userManager;

    }

  }
}
