using NovelReader.Common.ModelDtos.Comment;

namespace NovelReader.API.Models
{
	public class Comment : CommentBase
	{
		public User User { get; set; } = new User();
		public Novel Novel { get; set; } = new Novel();
	}
}
