using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DTOs {
	public class UserDto {
		public int Id { get; set; }
		public string Name { get; set; }
		public double? Rating { get; set; }
		public string Role { get; set; }
		public virtual IEnumerable<TeamDTO> TeamUsers { get; set; }
	}
}
