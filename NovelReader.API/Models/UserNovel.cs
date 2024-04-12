namespace NovelReader.API.Models
{
	public class UserNovel
	{
		public string UserId { get; set; } = string.Empty;
		public string NovelId { get; set; } = string.Empty;

		public int CurrentChapter { get; set; } = 1;
		public DateTime LastReadAt { get; set; } = DateTime.Now;

		public User User { get; set; } = null!;
		public Novel Novel { get; set; } = null!;
	}
}
