using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services;
using Database.Models;
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

		[HttpGet("{userId}")]
	    public Task<IEnumerable<Project>> GetByUserId(int userId) {
		    return projectService.GetByUserId(userId);
	    }

	    [HttpPost]
	    public Task<Project> AcceptInvitation([FromBody]Project project) {
		    return projectService.PostAsync(project);
	    }
	}
}