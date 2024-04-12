using NovelReader.Common.ModelDtos.Novel;
using NovelReader.Common.ModelDtos.User;

namespace NovelReader.Common.ModelDtos.Rate
{
	public class RateDto: RateBase
	{
		public bool IsEdited { get; set; } = false;
		public UserDto User { get; set; } = new UserDto();
		public NovelDto Novel { get; set; } = new NovelDto();
	}
}
