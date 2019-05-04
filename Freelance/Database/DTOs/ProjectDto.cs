using System.Collections.Generic;

namespace Database.DTOs {
	public class ProjectDto {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public virtual string Owner { get; set; }
		public virtual string Category { get; set; }
		public virtual IEnumerable<string> ProjectTeams { get; set; }
		public virtual IEnumerable<string> Tags { get; set; }
		public virtual IEnumerable<string> Tasks { get; set; }
		public virtual IEnumerable<string> Reports { get; set; }
	}
}