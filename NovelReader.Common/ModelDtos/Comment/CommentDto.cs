using NovelReader.Common.ModelDtos.Novel;
using NovelReader.Common.ModelDtos.User;

namespace NovelReader.Common.ModelDtos.Comment
{
	public class CommentDto : CommentBase
	{
		public bool IsEdited { get; set; } = false;
		public UserDto User { get; set; } = new UserDto();
	}
}
