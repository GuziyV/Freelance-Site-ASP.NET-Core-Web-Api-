using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.DTOs;
using Database.Models;
using Database.Services;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace BL.Services {
	public class UserService : CRUDService<User> {
		public UserService(DbContext context) : base(context) {
		}

		public async Task<bool> AcceptInvitation(int userId, int teamId) {
			var teamUser = await this.context.Set<TeamUser>().Where(tu => tu.IsActivated == false && tu.TeamId == teamId && tu.UserId == userId).FirstOrDefaultAsync();
			if (teamUser == null) {
				return false;
			}
			else {
				teamUser.IsActivated = true;
			}

			await context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<User>> GetUsersByTask(string taskName) {
			return (await context.Set<Team>().Where(t => t.Tasks.Any(ta => ta.Name == taskName)).SelectMany(te => te.TeamUsers.Select(tu => tu.User)).ToListAsync());
		}

		public async System.Threading.Tasks.Task<User> Authentificate(string login, string password) {

			if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
				throw new ArgumentNullException("Username or password can not be empty");

			var user = (await context.Set<User>().SingleOrDefaultAsync(u => u.Name == login));
			if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
				throw new ArgumentException("Wrong email or password");
			}
			await context.SaveChangesAsync();
			return user;
		}

		public async Task<IEnumerable<dynamic>> GetNumberOfTeamsForEachUser() {
			return await context.Set<Team>().GroupBy(t => t.ProjectTeams)
				.Select(r => new { Team = r.Key.Select(pt => pt.Project.Owner), Count = r.Count() }).ToListAsync();
		}

		public async Task<User> Register(RegisterUserDTO entity) {
			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(entity.Password, passwordHash: out passwordHash, passwordSalt: out passwordSalt);
			var user = new User();
			user.Name = entity.Login;
			user.Role = entity.Role;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;
			var u = await PostAsync(user);
			return u;
		}

		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
			if (password == null) throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

			using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) {
			if (password == null) throw new ArgumentNullException("password");
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
			if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
			if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

			using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt)) {
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++) {
					if (computedHash[i] != storedHash[i]) return false;
				}
			}

			return true;
		}
	}
}
