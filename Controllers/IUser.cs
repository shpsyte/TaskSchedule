using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskSchedule.Data;

public interface IUser {

  int Id ();
  string UserName ();
  string Email ();
  byte[] AvatarImage ();
  IEnumerable<Claim> GetClaimsIdentity ();
  bool IsAdmin ();

}

public class User : IUser {

  private readonly IHttpContextAccessor _accessor;
  private ApplicationDbContext _context;

  public User (IHttpContextAccessor acessor, ApplicationDbContext context) {
    this._accessor = acessor;
    this._context = context;
    // this._userManager = userManager;
  }

  public byte[] AvatarImage () {
    return _accessor.HttpContext.Session.Get ("User.Settings.AvatarImage");
  }

  public string Email () {
    return _accessor.HttpContext.User.Identity.Name;
  }

  public int Id () {
    return System.Convert.ToInt32 (_accessor.HttpContext.User.FindFirstValue (ClaimTypes.NameIdentifier));
  }

  public string UserName () {
    return _accessor.HttpContext.User.Identity.Name;
  }

  public IEnumerable<Claim> GetClaimsIdentity () {
    return _accessor.HttpContext.User.Claims;
  }

  public bool IsAdmin () {
    return _accessor.HttpContext.User.IsInRole ("ADMINISTRATOR");
  }
}
