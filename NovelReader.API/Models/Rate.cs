using NovelReader.Common.ModelDtos.Rate;

namespace NovelReader.API.Models
{
	public class Rate: RateBase
	{
		public User User { get; set; } = new User();
		public Novel Novel { get; set; } = new Novel();
	}
}
