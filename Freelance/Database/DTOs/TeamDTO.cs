using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DTOs {
	public class TeamDTO {
		public string Name { get; set; }
		public string CreatedBy { get; set; }
		public IEnumerable<string> Users { get; set; }
		public virtual IEnumerable<string> Projects { get; set; }
		public virtual IEnumerable<string> Tasks { get; set; }
	}
}
