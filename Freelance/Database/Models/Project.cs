using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models {
	public class Project : Entity {
		public string Title { get; set; }
		public string Description { get; set; }
		public virtual User Owner { get; set; }
		public int OwnerId { get; set; }
		public virtual Category Category { get; set; }
		public virtual ICollection<ProjectTeam> ProjectTeams { get; set; }
		public virtual  ICollection<Tag> Tags { get; set; }
		public virtual ICollection<Task> Tasks { get; set; }
	}
}
