namespace NovelReader.Common.ModelDtos.Comment
{
	public class CommentBase
	{
		public string Id { get; set; } = string.Empty;
		public string UserId { get; set; } = string.Empty;
		public string NovelId { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
	}
}
