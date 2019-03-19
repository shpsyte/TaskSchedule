using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskSchedule.Domain;

namespace TaskSchedule.Data {
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int> {

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) {

    }

    public DbSet<TaskUser> TaskUser { get; set; }
    protected override void OnModelCreating (ModelBuilder builder) {
      base.OnModelCreating (builder);

      builder.Entity<ApplicationUser> ().ToTable ("User");
      builder.Entity<ApplicationUser> ().HasKey (a => a.Id);
      builder.Entity<ApplicationRole> ().ToTable ("Role");
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

      builder.Entity<TaskUser> ().ToTable ("TaskUser");
      builder.Entity<TaskUser> ().HasKey (a => new { a.Id });

      builder.Entity<TaskUser> ().HasOne (d => d.User)
        .WithMany (p => p.TaskUser)
        .HasForeignKey (d => d.UserId)
        .OnDelete (DeleteBehavior.Restrict);

      builder.Entity<TaskUser> ().HasOne (d => d.Location)
        .WithMany (p => p.TaskUser)
        .HasForeignKey (d => d.LocationId)
        .OnDelete (DeleteBehavior.SetNull);

    }
  }

}
