using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services {
	public class TeamService : CRUDService<Team> {
		public TeamService(DbContext context) : base(context) {
		}

		public async Task<IEnumerable<Team>> GetTeamByTagName(string tagName) {
			return await context.Set<Team>().Where(t => t.Tasks.Any(task => task.Tags.Any(tag => tag.Name == tagName))).ToListAsync();
		}
	}
}
