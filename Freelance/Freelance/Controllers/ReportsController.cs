using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services;
using Database.DTOs;
using Database.Models;
using Database.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
	    private readonly ReportService reportService;

	    public ReportsController(ReportService reportService) {
		    this.reportService = reportService;
	    }

		[HttpGet]
	    public async Task<IEnumerable<ReportDto>> GetAll(int teamId) {
		    var all = (await reportService.GetReportByTeam(teamId));
		    return all.ConvertAll();
	    }

	    [HttpPost]
	    public async Task<ReportDto> CreateReport([FromBody]Report report) {

		    return (await reportService.PostAsync(report)).Convert();
	    }
	}
}