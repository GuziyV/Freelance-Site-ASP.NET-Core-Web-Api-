using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DTOs {
	public class TaskDto {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<string> Tags { get; set; }
		public bool IsClosed { get; set; }
		public TeamDTO Team { get; set; }
		public int? ProjectId { get; set; }
		public int StoryPoints { get; set; }
		public int AvailableStoryPoints { get; set; }
		public int CompletedStoryPoints { get; set; } = 1;
	}
}
