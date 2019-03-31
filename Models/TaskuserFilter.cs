using System;

namespace TaskSchedule.Models {
  public class TaskuserFilter {
    public TaskuserFilter () {
      this.isAdmin = false;
    }

    public TaskuserFilter (bool admin) : this () {
      this.isAdmin = admin;
    }
    public Nullable<DateTime> _ini { get; set; }
    public Nullable<DateTime> _fim { get; set; }
    public string studantName { get; set; }
    public Nullable<Boolean> done { get; set; }
    public Boolean isAdmin { get; set; }
    public Int32? userId { get; set; }
    public Int32? locationId { get; set; }

  }
}
