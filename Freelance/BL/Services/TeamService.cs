﻿using Database.DTOs;
using Database.Models;
using Database.Services;
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

		public async Task<Team> ChangeInviteStatus(int userId, int teamId, bool status) {
			var teamUser = await context.Set<TeamUser>().FirstOrDefaultAsync(t => t.UserId == userId && t.TeamId == teamId);
			if (teamUser == null) {
				throw new NullReferenceException("TeamUser can not be null");
			}

			if (status) {
				teamUser.IsActivated = true;
				teamUser.IsDeclined = false;
			}
			else {
				teamUser.IsDeclined = true;
				teamUser.IsActivated = false;
			}

			await context.SaveChangesAsync();
			return teamUser.Team;
		}

		public async Task<Team> Convert(CreateTeamDto team) {
			List<TeamUser> teamUsers = new List<TeamUser>();
			foreach (var teamUserId in team.UserIds) {
				teamUsers.Add(new TeamUser() {
					UserId = teamUserId,
				});
			}

			List<ProjectTeam> projectTeams = new List<ProjectTeam>();
			foreach (var projectTeamsIds in team.ProjectIds) {
				projectTeams.Add(new ProjectTeam() {
					ProjectId = projectTeamsIds,
				});
			}
			var dbTeam = new Team() {
				Name =  team.Name,
				CreatedBy = await context.Set<User>().FirstOrDefaultAsync(u => u.Id == team.CreatedById),
				TeamUsers = teamUsers,
				ProjectTeams = projectTeams,
			};

			return dbTeam;

		}
	}
}
