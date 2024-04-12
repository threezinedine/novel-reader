using NovelReader.Common.ModelDtos.Novel;

namespace NovelReader.API.Models
{
	public class Novel : NovelBase
	{
		public User Author { get; set; } = null!;
		public List<Chapter> Chapters { get; set; } = null!;
		public List<Comment> Comments { get; set; } = null!;
		public List<Rate> Rate { get; set; } = null!;
		public List<Tag> Tags { get; set; } = null!;
		public List<UserNovel> Readers { get; set; } = null!;
	}
}
