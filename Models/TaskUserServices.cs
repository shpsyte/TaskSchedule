using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskSchedule.Data;
using TaskSchedule.Domain;

namespace TaskSchedule.Models {
  public class TaskUserServices {
    private ApplicationDbContext _context;
    public TaskUserServices (ApplicationDbContext context) {
      _context = context;
    }
    public async Task<IQueryable<TaskUser>> GetTaskAsync (TaskuserFilter p) {

      var dataUser = _context.TaskUser
        .Where (a => a.Done == p.done)
        .Include (a => a.User)
        .AsNoTracking ()
        .AsQueryable ();

      p.userId = p.isAdmin ? p.userId : p.CurrentUserId;

      if (p._ini.HasValue) {
        dataUser = p._ini.HasValue ? dataUser.Where (a => a.DateOfTest.Date >= p._ini.Value.Date) : dataUser;
      }
      if (p._fim.HasValue) {
        dataUser = dataUser.Where (a => a.DateOfTest.Date <= p._fim.Value.Date);
      }
      if (!String.IsNullOrEmpty (p.studantName)) {
        dataUser = dataUser.Where (a => a.StudentName.ToLower ().Contains (p.studantName.ToLower ()));
      }

      if (p.userId.HasValue) {
        dataUser = dataUser.Where (a => a.UserId == p.userId.Value);
      }

      if (p.locationId.HasValue) {
        dataUser = dataUser.Where (a => a.LocationId == p.locationId.Value);
      }

      return dataUser;

    }

  }
}
