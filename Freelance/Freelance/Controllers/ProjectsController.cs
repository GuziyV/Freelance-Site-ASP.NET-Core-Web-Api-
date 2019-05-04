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

namespace Freelance.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
	[ApiController]
    public class ProjectsController : ControllerBase {
	    private readonly ProjectService projectService;

	    public ProjectsController(ProjectService projectService) {
		    this.projectService = projectService;
		}

		[HttpGet]
	    public async Task<IEnumerable<ProjectDto>> GetByUserId(int userId) {
		    return (await projectService.GetByUserId(userId)).ConvertAll();
	    }

	    [HttpPost]
	    public async Task<ProjectDto> CreateProject([FromBody]CreateProjectDTO project) {
			var claimsIdentity = this.User.Identity as ClaimsIdentity;
			project.UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);

			return (await projectService.PostAsync(await projectService.Convert(project))).Convert();
	    }

	    [HttpDelete("{projectId}")]
	    public async Task<ProjectDto> DeleteProject(int projectId) {
		    var claimsIdentity = this.User.Identity as ClaimsIdentity;
		    var UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);

		    return (await projectService.DeleteProject(projectId, UserId)).Convert();
	    }
	}
}