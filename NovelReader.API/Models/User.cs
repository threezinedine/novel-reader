using NovelReader.Common.ModelDtos.User;

namespace NovelReader.API.Models
{
	public class User: UserBase
	{
		public string HashedPassword { get; set; } = string.Empty;
		public List<Novel> OwnedNovels { get; set; } = new List<Novel>();
		public List<UserNovel> ReadNovels { get; set; } = new List<UserNovel>();
	}
}
