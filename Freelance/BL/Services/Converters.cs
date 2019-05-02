using Database.DTOs;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Services {
	public static class Converters {
		public static TeamDTO Convert(this Team team) {
			var teamDto = new TeamDTO() {
				CreatedBy = team.CreatedBy.Name,
				Name = team.Name,
				Users = team.TeamUsers.Select(u => u.User.Name),
				Tasks = team.Tasks.Select(u => u.Name),
				Projects = team.ProjectTeams.Select(u => u.Project.Title),
			};

			return teamDto;
		}

		public static IEnumerable<TeamDTO> ConvertAll(this IEnumerable<Team> team) {
			return team.Select(t => t.Convert());
		}
	}
}
