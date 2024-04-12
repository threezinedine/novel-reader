using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace NovelReader.UI.Common.Shared
{
    public partial class Navbar
    {
        [Inject]
        private AuthenticationStateProvider m_AuthStateProvider { get; set; } = null!;
        [Inject]
        private NavigationManager m_NavigationManager { get; set; } = null!;

        private AuthenticationState m_State = new AuthenticationState(new ClaimsPrincipal());

        protected override async Task OnInitializedAsync()
        {
            m_State = await m_AuthStateProvider.GetAuthenticationStateAsync();
        }
    }
}
