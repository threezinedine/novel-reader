using Microsoft.AspNetCore.Components;
using NovelReader.Common.ModelDtos.User;
using NovelReader.UI.Common.Services.RequestService;
using System.Net.Http.Json;

namespace NovelReader.UI.Common.ViewModels
{
	public class LoginViewModel
	{
		private readonly IRequestService m_RequestService;
        private UserDto m_User { get; set; } = new UserDto();

        public LoginViewModel(IRequestService requestService)
        {
            m_RequestService = requestService; 
        }

        public string Username
        {
            get => m_User.Username;
            set
            {
                if (m_User.Username != value)
                {
					m_User.Username = value;
				}
            }
        }

        public string Password
        {
			get => m_User.Password;
			set
            {
				if (m_User.Password != value)
                {
					m_User.Password = value;
                }
            }
        }

        public string? ValidateUsername(string username)
        {
            return null;
        }
        public async Task<string?> Login()
        {
            var response = await m_RequestService
                                .GetHttpClient().PostAsJsonAsync("user/login", m_User);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await m_RequestService.SaveToken(token); 
                return null;
            }
            else
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
