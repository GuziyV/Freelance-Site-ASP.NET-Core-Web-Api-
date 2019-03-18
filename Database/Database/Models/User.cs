using System;
using System.Collections.Generic;
using System.Text;
using Database.Enums;

namespace Database.Models {
	public class User : Entity {
		public string Name { get; set; }
		public double? Rating { get; set; }
		public virtual UserType UserType { get; set; }
		public virtual ICollection<TeamUser> TeamUsers { get; set; }
	}
}
