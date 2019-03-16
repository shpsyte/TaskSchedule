using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskSchedule.Data {
  public class ApplicationDbContext : IdentityDbContext<IdentityUser> {
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

    protected override void OnModelCreating (ModelBuilder builder) {
      base.OnModelCreating (builder);

      builder.Entity<IdentityUser> ().ToTable ("User");
      builder.Entity<IdentityUser> ().HasKey (a => a.Id);
      builder.Entity<IdentityRole> ().ToTable ("Role");
      builder.Entity<IdentityUserClaim<string>> ().ToTable ("UserClaim");
      builder.Entity<IdentityUserRole<string>> ().ToTable ("UserRole");
      builder.Entity<IdentityUserLogin<string>> ().ToTable ("UserLogin");
      builder.Entity<IdentityRoleClaim<string>> ().ToTable ("RoleClaim");
      builder.Entity<IdentityUserToken<string>> ().ToTable ("UserToken");
      builder.Entity<IdentityUserRole<string>> ().HasKey (a => new { a.UserId, a.RoleId });
      builder.Entity<IdentityUserClaim<string>> ().HasKey (a => new { a.UserId, a.Id });
      builder.Entity<IdentityUserLogin<string>> ().HasKey (a => new { a.UserId, a.ProviderKey });
      builder.Entity<IdentityRoleClaim<string>> ().HasKey (a => new { a.RoleId, a.Id });
      builder.Entity<IdentityUserToken<string>> ().HasKey (a => new { a.UserId });
    }
  }

}
