using NovelReader.Common.ModelDtos.Chapter;
using NovelReader.Common.ModelDtos.Comment;
using NovelReader.Common.ModelDtos.Rate;
using NovelReader.Common.ModelDtos.Tag;
using NovelReader.Common.ModelDtos.User;

namespace NovelReader.Common.ModelDtos.Novel
{
	public class NovelDto: NovelBase
	{
		public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
		public List<RateDto> Rate { get; set; } = new List<RateDto>();
		public List<TagDto> Tags { get; set; } = new List<TagDto>(); 
	}
}
