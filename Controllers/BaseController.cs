using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskSchedule.Data;
using TaskSchedule.Domain;
using TaskSchedule.Models;

namespace TaskSchedule.Controllers {

  [Authorize]
  public class BaseController : Controller {

    public ApplicationDbContext _context;
    public ILogger<BaseController> _logger;
    public UserManager<ApplicationUser> _userManager;

    public BaseController (UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<BaseController> logger) {
      _context = context;
      _logger = logger;
      _userManager = userManager;

    }

  }
}
