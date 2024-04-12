namespace NovelReader.Common.ModelDtos.Rate
{
	public class RateBase
	{
		public string Id { get; set; } = string.Empty;
		public string UserId { get; set; } = string.Empty;
		public string NovelId { get; set; } = string.Empty;
		public int Rate { get; set; } = 0;
		public string Comment { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public DateTime UpdatedAt { get; set; } = DateTime.Now;
	}
}
