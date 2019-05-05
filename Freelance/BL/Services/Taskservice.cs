using System;
using System.Threading.Tasks;
using Database.DTOs;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Services {
	public class TaskService : CRUDService<Database.Models.Task> {
		public TaskService(DbContext context) : base(context) {
		}

		public override async Task<Database.Models.Task> PostAsync(Database.Models.Task entity) {
			var resEntity = context != null ? (await context.Set<Database.Models.Task>().AddAsync(entity)).Entity : null;
			await context.SaveChangesAsync();
			return (await context.Set<Database.Models.Task>()
				.Include(p => p.Team)
				.SingleOrDefaultAsync(proj => proj.Id == resEntity.Id));
		}
	}
}
