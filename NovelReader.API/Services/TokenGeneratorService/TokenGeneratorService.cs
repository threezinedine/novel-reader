using Microsoft.IdentityModel.Tokens;
using NovelReader.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NovelReader.API.Services.TokenGeneratorService
{
	public class TokenGeneratorService : ITokenGeneratorService
	{
		private readonly IConfiguration m_Configuration;
		private readonly string m_Key = "JWT:SecretKey";
        public TokenGeneratorService(IConfiguration configuration)
        {
			m_Configuration = configuration; 
        }
        public string GenerateToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(m_Configuration.GetValue<string>(m_Key)!);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id),
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.Role, user.Role.ToString()),
				}),
				Expires = DateTime.UtcNow.AddHours(8),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
															SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
