using System;
using System.Collections.Generic;
using System.Text;
using Database.DTOs;

namespace Database.Models {
	public class Task : Entity {
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
		public bool IsClosed { get; set; }
		public virtual Team Team { get; set; }
		public virtual int? TeamId { get; set; }
		public virtual ICollection<Report> Reports { get; set; }
		public virtual Project Project { get; set; }
		public int? ProjectId { get; set; }
		public int StoryPoints { get; set; }
	}
}
