using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BL.Services;
using Database.DTOs;
using Database.Enums;
using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TeamsController : ControllerBase {
		private readonly TeamService teamService;

		public TeamsController(TeamService teamService) {
			this.teamService = teamService;
		}

		[HttpGet]
		public async Task<IEnumerable<TeamDTO>> GetAll() {
			var claimsIdentity = this.User.Identity as ClaimsIdentity;
			var UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
			var role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
			var all = role == Role.Manager ? (await teamService.GetAllAsync()).Where(t => t.CreatedBy.Id == UserId) :
				(await teamService.GetAllAsync()).Where(t => t.TeamUsers.Any(u => u.UserId == UserId && u.IsDeclined == false));
			return all.ConvertAll(UserId);
		}

		[HttpGet("{tag}")]
		public async Task<IEnumerable<TeamDTO>> GetAll(string tag) {
			return (await teamService.GetTeamByTagName(tag)).ConvertAll(0);
		}

		[HttpPost("{teamId}/invite/{status}")]
		public async Task<TeamDTO> ChangeInviteStatus(int teamId, bool status) {
			var claimsIdentity = this.User.Identity as ClaimsIdentity;
			var UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);

			return (await teamService.ChangeInviteStatus(UserId, teamId, status)).Convert(UserId);
		}

		[HttpPost]
		public async Task<TeamDTO> CreateTeam([FromBody] CreateTeamDto team) {
			var claimsIdentity = this.User.Identity as ClaimsIdentity;
			var UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
			team.CreatedById = UserId;

			return (await teamService.PostAsync(await teamService.Convert(team))).Convert(UserId);
		}

	}
}