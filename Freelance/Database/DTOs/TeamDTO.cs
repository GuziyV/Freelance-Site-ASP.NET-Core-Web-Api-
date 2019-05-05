using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DTOs {
	public class TeamDTO {
		public int Id { get; set; }
		public string Name { get; set; }
		public string CreatedBy { get; set; }
		public IEnumerable<string> Users { get; set; }
		public virtual IEnumerable<ProjectDto> Projects { get; set; }
		public virtual IEnumerable<string> Tasks { get; set; }
	}
}
