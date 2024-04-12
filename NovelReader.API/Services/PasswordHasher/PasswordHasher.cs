using System.Security.Cryptography;
using System.Text;

namespace NovelReader.API.Services.PasswordHasher
{
	public class PasswordHasher : IPasswordHasher
	{
		private readonly IConfiguration m_Configuration;
		private readonly string m_Key = "SecretKey";
        public PasswordHasher(IConfiguration configuration)
        {
			m_Configuration = configuration; 
        }
        public string HashPassword(string password)
		{
			using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(m_Configuration.GetValue<string>(m_Key)!)))
			{
				var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				var passwordHash = Convert.ToBase64String(hashBytes);
				return passwordHash;
			}
		}

		public bool VerifyPassword(string password, string hashedPassword)
		{
			var newHashedPassword = HashPassword(password);
			return newHashedPassword == hashedPassword;
		}
	}
}
