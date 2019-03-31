using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskSchedule.Data;
using TaskSchedule.Domain;
using TaskSchedule.Models;

public class taskModel {
  public TaskuserFilter filter { get; set; }
  public IQueryable<TaskUser> tasks { get; set; }

}
