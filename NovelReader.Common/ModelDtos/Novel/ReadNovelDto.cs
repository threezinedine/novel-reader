using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReader.Common.ModelDtos.Novel
{
	public class ReadNovelDto
	{
		public string NovelId { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public int TotalChapters { get; set; } = 1;
		public int CurrentChapter { get; set; } = 1;
		public DateTime LastReadAt { get; set; } = DateTime.Now;
	}
}
