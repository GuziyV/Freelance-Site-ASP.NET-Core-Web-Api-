using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models {
	public class Report : Entity {
		public string Content { get; set; }
		public virtual Team Team { get; set; }
		public int? TeamId { get; set; }
		public virtual Task Task { get; set; }
		public int? TaskId { get; set; }
		public virtual User User { get; set; }
		public int? UserId { get; set; }
		public virtual Project Project { get; set; }
		public int? ProjectId { get; set; }
	}
}
