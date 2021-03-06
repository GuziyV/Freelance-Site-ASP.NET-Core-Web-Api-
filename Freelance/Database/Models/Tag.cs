﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models {
	public class Tag : Entity {
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual Project Project { get; set; }
		public int? ProjectId { get; set; }
		public virtual Task Task { get; set; }
		public int? TaskId { get; set; }

	}
}
