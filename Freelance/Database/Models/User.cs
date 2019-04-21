using System;
using System.Collections.Generic;
using System.Text;
using Database.Enums;

namespace Database.Models {
	public class User : Entity {
		public string Name { get; set; }
		public double? Rating { get; set; }
		public string Role { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public virtual ICollection<TeamUser> TeamUsers { get; set; }
	}
}
