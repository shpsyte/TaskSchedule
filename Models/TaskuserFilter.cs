using System;

namespace TaskSchedule.Models {
  public class TaskuserFilter {
    public TaskuserFilter () {
      this.isAdmin = false;
      this.CurrentUserId = 0;
    }

    public TaskuserFilter (bool admin, int CurrentUserId) {
      this.isAdmin = admin;
      this.CurrentUserId = CurrentUserId;
      this.done = false;
    }

    public TaskuserFilter (bool admin, int CurrentUserId, bool done) : this (admin, CurrentUserId) {
      this.done = done;
    }
    public Boolean isAdmin { get; set; }
    public Int32 CurrentUserId { get; set; }

    public Nullable<DateTime> _ini { get; set; }
    public Nullable<DateTime> _fim { get; set; }
    public string studantName { get; set; }
    public Boolean done { get; set; }
    public Int32? userId { get; set; }
    public Int32? locationId { get; set; }

  }
}
