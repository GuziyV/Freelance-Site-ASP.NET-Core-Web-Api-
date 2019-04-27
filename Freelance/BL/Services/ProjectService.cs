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
				.Include(p => p.Tasks)/*.Where(p => p.Owner.Id == userId)*/.ToListAsync();
		}

		public Project Convert(CreateProjectDTO projectDto) {
			var category = context.Set<Category>().FirstOrDefault(c => c.Name == projectDto.Category);
			if (category == null) {
				throw new ArgumentNullException("Unknown category");
			}

			return new Project() {
				Title = projectDto.Title,
				Category = category,
				Tags = projectDto.Tags,
				Description = projectDto.Description,
			};
		}

		public override async Task<Project> PostAsync(Project entity) {
			var resEntity = context != null ? (await context.Set<Project>().AddAsync(entity)).Entity : null;
			await context.SaveChangesAsync();
			return await context.Set<Project>()
				.Include(p => p.ProjectTeams)
				.Include(p => p.Reports)
				.Include(p => p.Tasks)
				.SingleOrDefaultAsync(proj => proj.Id == resEntity.Id);
		}
	}
}
