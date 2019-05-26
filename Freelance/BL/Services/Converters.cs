using Database.DTOs;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Services {
	public static class Converters {
		public static TeamDTO Convert(this Team team, int userId) {
			var teamDto = new TeamDTO() {
				Id = team.Id,
				CreatedBy = team.CreatedBy?.Name,
				Name = team.Name,
				Users = team.TeamUsers?.Select(u => u.User?.Name),
				Tasks = team.Tasks?.Select(u => u.Name),
				Projects = team.ProjectTeams?.Select(u => u.Project?.Convert()),
				IsAccepted = team.TeamUsers.Any(u => u.UserId == userId && u.IsActivated == true)
			};

			return teamDto;
		}

		public static IEnumerable<TeamDTO> ConvertAll(this IEnumerable<Team> team, int userId) {
			return team.Select(t => t.Convert(userId));
		}

		public static ProjectDto Convert(this Project project) {
			var projectDto = new ProjectDto() {
				Id =project.Id,
				Owner = project.Owner.Name,
				Title = project.Title,
				Description = project.Description,
				Category = project.Category?.Name,
				ProjectTeams = project.ProjectTeams?.Select(u => u.Team.Name),
				Tasks = project.Tasks?.Select(u => u.Name),
				Tags = project.Tags?.Select(t => t.Name),
				Reports = project.Reports?.ConvertAll(),
			};

			return projectDto;
		}

		public static IEnumerable<ProjectDto> ConvertAll(this IEnumerable<Project> team) {
			return team.Select(t => t.Convert());
		}

		public static UserDto Convert(this User user) {
			try {
				var userDto = new UserDto() {
				Id = user.Id,
				Name = user.Name,
				Role = user.Role,
				Rating = user.Rating,
				TeamUsers = user.TeamUsers?.Select(tu => tu?.Team)?.ConvertAll(user.Id)
			};
				return userDto;

			}
			catch (Exception e) {
				Console.WriteLine(e);
				throw;
			}
		}

		public static IEnumerable<UserDto> ConvertAll(this IEnumerable<User> team) {
			return team.Select(t => t.Convert());
		}

		public static TaskDto Convert(this Task task) {
			return new TaskDto() {
				Id = task.Id,
				Name = task.Name,
				Description = task.Description,
				Tags = task.Tags.Select(t => t.Name).ToList(),
				IsClosed = task.IsClosed,
				Team = task.Team?.Convert(0),
				ProjectId = task.ProjectId,
				StoryPoints = task.StoryPoints,
			};
		}

		public static IEnumerable<TaskDto> ConvertAll(this IEnumerable<Task> team) {
			return team.Select(t => t.Convert());
		}

		public static ReportDto Convert(this Report report) {
			return new ReportDto() {
				Content = report.Content,
				Team = report.Team?.Name,
				CompletedStoryPoints = report.CompletedStoryPoints,
			};
		}

		public static IEnumerable<ReportDto> ConvertAll(this IEnumerable<Report> team) {
			return team.Select(t => t.Convert());
		}
	}
}
