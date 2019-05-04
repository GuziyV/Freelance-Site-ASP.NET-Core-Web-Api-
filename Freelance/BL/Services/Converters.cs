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
				Id = team.Id,
				CreatedBy = team.CreatedBy?.Name,
				Name = team.Name,
				Users = team.TeamUsers?.Select(u => u.User?.Name),
				Tasks = team.Tasks?.Select(u => u.Name),
				Projects = team.ProjectTeams?.Select(u => u.Project?.Title),
			};

			return teamDto;
		}

		public static IEnumerable<TeamDTO> ConvertAll(this IEnumerable<Team> team) {
			return team.Select(t => t.Convert());
		}

		public static ProjectDto Convert(this Project project) {
			var projectDto = new ProjectDto() {
				Id =project.Id,
				Owner = project.Owner.Name,
				Title = project.Title,
				Description = project.Description,
				Category = project.Category?.Name,
				ProjectTeams = project.ProjectTeams?.Select(u => u.Team.Name),
				Tasks= project.Tasks?.Select(u => u.Name),
				Reports = project.Reports?.Select(u => u.Content),
				Tags = project.Tags?.Select(t => t.Name),
			};

			return projectDto;
		}

		public static IEnumerable<ProjectDto> ConvertAll(this IEnumerable<Project> team) {
			return team.Select(t => t.Convert());
		}

		public static UserDto Convert(this User user) {
			var userDto = new UserDto() {
				Id = user.Id,
				Name = user.Name,
				Role = user.Role,
				Rating = user.Rating,
				TeamUsers = user.TeamUsers?.Select(tu => tu?.Team).ConvertAll()
			};

			return userDto;
		}

		public static IEnumerable<UserDto> ConvertAll(this IEnumerable<User> team) {
			return team.Select(t => t.Convert());
		}
	}
}
