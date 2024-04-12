namespace NovelReader.Common.ModelDtos.Novel
{
	public class NovelBase
	{
		public string Id { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string CoverURL { get; set; } = string.Empty;
		public string AuthorId { get; set; } = string.Empty;
		public string AvatarURL { get; set; } = string.Empty;
		public bool IsCompleted { get; set; } = false;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
