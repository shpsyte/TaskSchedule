using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TaskSchedule.Domain {
  public class ApplicationUser : IdentityUser<int> {
    public string Name { get; set; }
    public byte[] Photo { get; set; }
    public virtual List<TaskUser> TaskUser { get; set; }

  }

  public class ApplicationRole : IdentityRole<int> {

  }
}
