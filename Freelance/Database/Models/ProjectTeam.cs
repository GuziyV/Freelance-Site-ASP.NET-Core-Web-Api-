namespace Database.Models {
	public class ProjectTeam : Entity {
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
		public int TeamId { get; set; }
		public virtual Team Team { get; set; }	}
}