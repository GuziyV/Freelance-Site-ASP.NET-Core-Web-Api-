using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models {
	public class Report : Entity {
		public string Content { get; set; }
		public virtual Team Team { get; set; }
		public virtual Task Task { get; set; }
	}
}
