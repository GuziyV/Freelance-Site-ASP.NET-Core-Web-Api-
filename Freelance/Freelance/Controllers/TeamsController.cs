using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BL.Services;
using Database.DTOs;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.Controllers {
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class TeamsController : ControllerBase {
		private readonly TeamService teamService;

		public TeamsController(TeamService teamService) {
			this.teamService = teamService;
		}

		[HttpGet]
		public async Task<IEnumerable<Team>> GetByUserId(string tag) {
			return await teamService.GetTeamByTagName(tag);
		}

		[HttpGet]
		public async Task<IEnumerable<Team>> GetAll() {
			return await teamService.GetAllAsync();
		}

		[HttpPost]
		public async Task<Team> DeleteProject([FromBody]CreateTeamDto team) {
			var claimsIdentity = this.User.Identity as ClaimsIdentity;
			var UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
			team.CreatedById = UserId;

			return await teamService.PostAsync(await teamService.Convert(team));
		}
	}
}