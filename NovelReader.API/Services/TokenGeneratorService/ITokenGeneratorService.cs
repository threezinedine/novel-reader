using NovelReader.API.Models;

namespace NovelReader.API.Services.TokenGeneratorService
{
	public interface ITokenGeneratorService
	{
		public string GenerateToken(User user);
	}
}
