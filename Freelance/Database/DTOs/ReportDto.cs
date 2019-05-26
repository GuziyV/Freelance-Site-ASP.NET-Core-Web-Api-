namespace Database.DTOs {
	public class ReportDto {
		public int Id { get; set; }
		public string Team { get; set; }
		public TaskDto Task { get; set; }
		public UserDto UserDto { get; set; }
		public string Content { get; set; }
		public int CompletedStoryPoints { get; set; }
	}
}