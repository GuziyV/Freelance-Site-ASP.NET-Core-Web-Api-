using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Services {
	public class ProjectService : CRUDService<Project> {
		public ProjectService(DbContext context) : base(context) {
		}
	}
}
