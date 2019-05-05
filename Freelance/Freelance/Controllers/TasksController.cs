using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BL.Services;
using Database.DTOs;
using Database.Enums;
using Database.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
	    private readonly TaskService taskService;

	    public TasksController(TaskService projectService) {
		    this.taskService = projectService;
	    }
		[HttpPost]
	    public async Task<TaskDto> CreateTask([FromBody]Database.Models.Task task) {
		    return (await taskService.PostAsync(task)).Convert();
	    }

	    [HttpGet]
	    public async Task<IEnumerable<TaskDto>> GetAll() {
		    var claimsIdentity = this.User.Identity as ClaimsIdentity;
		    var UserId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
		    var role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
		    var all = role == Role.Manager ? (await taskService.GetAllAsync()).Where(t => t.Team.CreatedBy.Id == UserId) :
			    (await taskService.GetAllAsync()).Where(t => t.Team.TeamUsers.Any(u => u.UserId == UserId));
		    return all.ConvertAll();
	    }

	}
}