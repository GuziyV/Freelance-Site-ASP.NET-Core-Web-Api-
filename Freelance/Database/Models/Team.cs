using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models {
	public class Team : Entity {
		public string Name { get; set; }
		public virtual User CreatedBy { get; set; }
		public virtual ICollection<TeamUser> TeamUsers { get; set; }
		public virtual ICollection<ProjectTeam> ProjectTeams { get; set; }

	}
}
