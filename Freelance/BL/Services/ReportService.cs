﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Services {
	public class ReportService : CRUDService<Report> {
		public ReportService(DbContext context) : base(context) {
		}

		public async Task<IEnumerable<Report>> GetReportByTeam(int id) {
			return (await this.GetAllAsync()).Where(r => r.Team.Id == id);
		}

		public async Task<IEnumerable<Report>> GetReportByProject(int projectId) {
			return await context.Set<Team>()
				.SelectMany(t => t.ProjectTeams
					.Where(pt => pt.Project.Id == projectId)
					.SelectMany(pt => pt.Project.Reports))
					.ToListAsync();
		}
	}
}