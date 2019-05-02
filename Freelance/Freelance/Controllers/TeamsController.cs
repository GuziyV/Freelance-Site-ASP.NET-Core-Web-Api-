using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BL.Services;
using Database.DTOs;
using Database.Models;
using Database.Services;
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
		public async Task<IEnumerable<TeamDTO>> GetAll() {
			var claimsIdentity = this.User.Identity as ClaimsIdentity;
			var UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
			var all = (await teamService.GetAllAsync()).Where(t => t.CreatedBy.Id == UserId);
			return all.ConvertAll();
		}

		[HttpPost]
		public async Task<TeamDTO> DeleteProject([FromBody]CreateTeamDto team) {
			var claimsIdentity = this.User.Identity as ClaimsIdentity;
			var UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
			team.CreatedById = UserId;

			return (await teamService.PostAsync(await teamService.Convert(team))).Convert();
		}
	}
}