using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DTOs {
	public class CreateTeamDto {
		public string Name { get; set; }
		public int CreatedById { get; set; }
		public IEnumerable<int> UserIds { get; set; }
		public IEnumerable<int> ProjectIds { get; set; }

	}
}
