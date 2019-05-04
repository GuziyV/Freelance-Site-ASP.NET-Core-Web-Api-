using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.DTOs;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace BL.Services {
	public class ProjectService : CRUDService<Project> {
		public ProjectService(DbContext context) : base(context) {
		}

		public async Task<IEnumerable<Project>> GetByUserId(int userId) {
			return await this.context.Set<Project>()
				.Include(p => p.ProjectTeams)
				.Include(p => p.Category)
				.Include(p => p.Reports)
				.Include(p => p.Tags)
				.Include(p => p.Tasks)
				.Where(p => p.Owner.Id == userId)
				.ToListAsync();
		}

		public async Task<Project> Convert(CreateProjectDTO projectDto) {
			var category = await context.Set<Category>().FirstOrDefaultAsync(c => c.Name == projectDto.Category);
			var user = await context.Set<User>().FirstOrDefaultAsync(u => u.Id == projectDto.UserId);
			if (category == null) {
				throw new NullReferenceException("Unknown category");
			}

			if (user == null) {
				throw new NullReferenceException("Unknown user");
			}

			return new Project() {
				Title = projectDto.Title,
				Category = category,
				Tags = projectDto.Tags,
				Description = projectDto.Description,
				Owner = user,
			};
		}

		public override async Task<Project> PostAsync(Project entity) {
			var resEntity = context != null ? (await context.Set<Project>().AddAsync(entity)).Entity : null;
			await context.SaveChangesAsync();
			return (await context.Set<Project>()
				.Include(p => p.ProjectTeams)
				.Include(p => p.Reports)
				.Include(p => p.Tasks)
				.SingleOrDefaultAsync(proj => proj.Id == resEntity.Id));
		}

		public async Task<Project> DeleteProject(int projectId, int userId) {
			if (context != null) {
				var entity = await context.Set<Project>().Include(t => t.Tags)
					.FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == userId);
				if (entity != null) {
					if (entity.Owner.Id != userId) {
						throw new MemberAccessException("You have no access to this data");
					}

					entity.Tags = null;
					var removed = context.Remove<Project>(entity).Entity;
					await context.SaveChangesAsync();
					return entity;
				}
			}
			throw new NullReferenceException("Context is missing by some reasons");
		}
	}
}
