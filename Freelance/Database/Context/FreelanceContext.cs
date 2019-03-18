using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Context {
	public class FreelanceContext : DbContext {
		public FreelanceContext(DbContextOptions<FreelanceContext> options)
			: base(options) { }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<ProjectTeam> ProjectTeams { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<TeamUser> TeamUsers { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Report> Reports { get; set; }
		public DbSet<Task> Tasks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<TeamUser>()
				.HasKey(bc => new { bc.UserId, bc.TeamId });
			modelBuilder.Entity<TeamUser>()
				.HasOne(bc => bc.User)
				.WithMany(b => b.TeamUsers)
				.HasForeignKey(bc => bc.UserId);
			modelBuilder.Entity<TeamUser>()
				.HasOne(bc => bc.Team)
				.WithMany(c => c.TeamUsers)
				.HasForeignKey(bc => bc.TeamId);
		}
	}
}
