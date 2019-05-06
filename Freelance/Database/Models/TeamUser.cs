namespace Database.Models {
	public class TeamUser {
		public int? TeamId { get; set; }
		public virtual Team Team { get; set; }
		public int? UserId { get; set; }
		public virtual User User { get; set; }
		public bool IsActivated { get; set; }
		public bool IsDeclined { get; set; }
	}
}