using Microsoft.AspNetCore.Identity;

namespace TaskSchedule.Domain {
  public class UserSetting {

    public UserSetting () {
      this.id = System.Guid.NewGuid ().ToString ();
    }
    public string id { get; set; }
    public string Name { get; set; }
    public byte[] PhotoProfile { get; set; }
    public IdentityUser User { get; set; }

  }
}
