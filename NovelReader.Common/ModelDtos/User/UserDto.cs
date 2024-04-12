using NovelReader.Common.ModelDtos.Novel;

namespace NovelReader.Common.ModelDtos.User
{
	public class UserDto: UserBase
	{
		public string Password { get; set; } = string.Empty;
		public List<NovelDto> OwnedNovels { get; set; } = new List<NovelDto>();
		public List<NovelDto> MarkedNovels { get; set; } = new List<NovelDto>();
	}
}
