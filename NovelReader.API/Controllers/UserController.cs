using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NovelReader.API.Context;
using NovelReader.API.Models;
using NovelReader.API.Services.PasswordHasher;
using NovelReader.API.Services.TokenGeneratorService;
using NovelReader.Common.ModelDtos.User;

namespace NovelReader.API.Controllers
{
	[ApiController]
	[Route("api/user")]
	public class UserController: ControllerBase
	{
		private readonly CustomDbContext m_DbContext;
		private readonly IMapper m_Mapper;
		private readonly IPasswordHasher m_PasswordHasher;
		private readonly ITokenGeneratorService m_TokenGeneratorService;
        public UserController(CustomDbContext customDbContext, IMapper mapper,
								IPasswordHasher passwordHasher,
								ITokenGeneratorService tokenGeneratorService)
        {
			m_DbContext = customDbContext; 
			m_Mapper = mapper;
			m_PasswordHasher = passwordHasher;
			m_TokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost("register")]
		public async Task<ActionResult<UserDto>> RegisterUser(UserDto userInfo)
		{
			var existedUser = await m_DbContext.Users
								.FirstOrDefaultAsync(x => x.Username == userInfo.Username);

			if (existedUser != null) return Conflict("Username is already existed");

			var newUser = new User
			{
				Id = Guid.NewGuid().ToString(),
				Username = userInfo.Username,
				HashedPassword = m_PasswordHasher.HashPassword(userInfo.Password),
			};

			m_DbContext.Users.Add(newUser);

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<UserDto>(newUser));
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserDto userInfo)
		{
			var existedUser = await m_DbContext.Users
								.FirstOrDefaultAsync(x => x.Username == userInfo.Username);

			if (existedUser == null) return NotFound("Username is not existed");

			if (!m_PasswordHasher.VerifyPassword(userInfo.Password, existedUser.HashedPassword))
			{
				return BadRequest("Password is not correct");
			}

			return Ok(m_TokenGeneratorService.GenerateToken(existedUser));
        }

		[HttpGet]
		[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<UserDto>> GetUserInfomation()
		{
			var username = User.Identity!.Name;
			var existedUser = await m_DbContext.Users
								.FirstOrDefaultAsync(x => x.Username == username);

			if (existedUser == null) return NotFound("User is not existed");

			return Ok(m_Mapper.Map<UserDto>(existedUser));
		}

		[HttpPost("register-admin")]
		public async Task<ActionResult<UserDto>> RegisterAdmin(UserDto userInfo)
		{
			var existedUser = await m_DbContext.Users
								.FirstOrDefaultAsync(x => x.Username == userInfo.Username);

			if (existedUser != null) return Conflict("Username is already existed");

			var newUser = new User
			{
				Id = Guid.NewGuid().ToString(),
				Username = userInfo.Username,
				HashedPassword = m_PasswordHasher.HashPassword(userInfo.Password),
				Role = Role.Admin,
			};

			m_DbContext.Users.Add(newUser);

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<UserDto>(newUser));
		}

		[HttpPut("change-role")]
		[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<ActionResult<UserDto>> ChangeUserRole(UserDto userInfo)
		{
			var existedUser = await m_DbContext.Users
								.FirstOrDefaultAsync(x => x.Id == userInfo.Id);

			if (existedUser == null) return NotFound("User is not existed");

			existedUser.Role = userInfo.Role;

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<UserDto>(existedUser));
		}

		[HttpPut]
		[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<UserDto>> UpdateUserInfomration(UserDto userInfo)
		{
			var existedUser = await m_DbContext.Users
								.FirstOrDefaultAsync(user => user.Username == User.Identity!.Name);

			if (existedUser == null) return NotFound("User is not existed");

			if (!userInfo.FirstName.IsNullOrEmpty())
			{
				existedUser.FirstName = userInfo.FirstName;
			}
			
			if (!userInfo.LastName.IsNullOrEmpty())
			{
				existedUser.LastName = userInfo.LastName;
			}

			if (!userInfo.Email.IsNullOrEmpty())
			{
				existedUser.Email = userInfo.Email;
			}

			existedUser.Gender = userInfo.Gender;

			if (!userInfo.PhoneNumber.IsNullOrEmpty())
			{
				existedUser.PhoneNumber = userInfo.PhoneNumber;
			}

			if (!userInfo.Address.IsNullOrEmpty())
			{
				existedUser.Address = userInfo.Address;
			}

			if (!userInfo.AvatarURL.IsNullOrEmpty())
			{
				existedUser.AvatarURL = userInfo.AvatarURL;
			}

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(m_Mapper.Map<UserDto>(existedUser));
		}

		[HttpDelete]
		[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
		public async Task<ActionResult> DeleteUser(UserDto userInfo)
		{
			var existedUser = await m_DbContext.Users
								.FirstOrDefaultAsync(x => x.Id == userInfo.Id);

			if (existedUser == null) return NotFound("User is not existed");

			m_DbContext.Users.Remove(existedUser);

			try
			{
				await m_DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}
	}
}
