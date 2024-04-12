using NovelReader.Common.ModelDtos.Novel;

namespace NovelReader.Common.ModelDtos.Chapter
{
	public class ChapterDto : ChapterBase
	{
		public NovelDto Novel { get; set; } = new NovelDto();
	}
}
