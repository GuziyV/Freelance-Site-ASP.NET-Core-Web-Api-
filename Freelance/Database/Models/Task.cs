using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models {
	public class Task : Entity {
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
		public bool IsClosed { get; set; }
	}
}
