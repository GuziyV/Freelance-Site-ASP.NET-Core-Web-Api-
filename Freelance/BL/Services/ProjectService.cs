using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Services {
	public class ProjectService : CRUDService<Project> {
		public ProjectService(DbContext context) : base(context) {
		}

		public async Task<IEnumerable<Project>> GetByUserId(int userId) {
			return await this.context.Set<Project>().Where(p => p.Owner.Id == userId).ToListAsync();
		}
	}
}
