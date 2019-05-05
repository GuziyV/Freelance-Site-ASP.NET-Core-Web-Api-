namespace Database.DTOs {
	public class ReportDto {
		public int Id { get; set; }
		public TeamDTO Team { get; set; }
		public TaskDto Task { get; set; }
		public UserDto UserDto { get; set; }
		public string Content { get; set; }
	}
}