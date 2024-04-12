using NovelReader.Common.ModelDtos.Tag;

namespace NovelReader.API.Models
{
	public class Tag : TagBase
	{
		public List<Novel> Novels { get; set; } = new List<Novel>();
	}
}
