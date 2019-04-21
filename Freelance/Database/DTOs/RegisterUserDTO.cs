using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DTOs {
	public class RegisterUserDTO {
		public string Password { get; set; }
		public string Login { get; set; }
		public string Role { get; set; }
	}
}
