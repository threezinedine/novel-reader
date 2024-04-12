using NovelReader.Common.ModelDtos.Chapter;

namespace NovelReader.API.Models
{
	public class Chapter: ChapterBase
	{
		public Novel Novel { get; set; } = new Novel();
	}
}
