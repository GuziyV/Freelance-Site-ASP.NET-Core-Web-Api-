using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models {
	public class Tag : Entity {
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
