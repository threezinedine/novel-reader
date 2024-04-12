namespace NovelReader.Common.ModelDtos.Chapter
{
	public class ChapterBase
	{
		public string Id { get; set; } = string.Empty;
		public string NovelId { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public int ChapterNumber { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string Content { get; set; } = string.Empty;
	}
}
