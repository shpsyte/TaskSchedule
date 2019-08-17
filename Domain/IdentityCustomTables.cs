using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TaskSchedule.Domain {
  public class ApplicationUser : IdentityUser<int> {
    public string Name { get; set; }
    public byte[] Photo { get; set; }
    public virtual List<TaskUser> TaskUser { get; set; }
    public string PasswordTip { get; set; }

    public bool IsAtivo () {
      return !this.EmailConfirmed;
    }

    [NotMapped]
    public bool IsAdmin { get; set; }

  }

  public class ApplicationRole : IdentityRole<int> {

  }
}
