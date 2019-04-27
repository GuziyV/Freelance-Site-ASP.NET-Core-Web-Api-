using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DTOs {
	public class CreateProjectDTO {
		public string Title { get; set; }
		public string Description { get; set; }
		public  string Category { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
	}
}
