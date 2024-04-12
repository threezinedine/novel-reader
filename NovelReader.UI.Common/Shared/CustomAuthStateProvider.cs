using Microsoft.AspNetCore.Components.Authorization;
using NovelReader.Common.ModelDtos.User;
using NovelReader.UI.Common.Services.RequestService;
using System.Net.Http.Json;
using System.Security.Claims;

namespace NovelReader.UI.Common.Shared
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IRequestService m_RequestService;

        public CustomAuthStateProvider(IRequestService requestService)
        {
            m_RequestService = requestService; 
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymous = new ClaimsIdentity();

            await m_RequestService.LoadToken();

            var response = await m_RequestService.GetHttpClient()
                                    .GetAsync("user");

            if (!response.IsSuccessStatusCode)
            {
                return new AuthenticationState(new ClaimsPrincipal(anonymous));
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>();

            if (user == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(anonymous));
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }, "apiauth");
            
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
