using Microsoft.AspNetCore.Components;
using NovelReader.UI.Common.Services.ToastService;
using NovelReader.UI.Common.ViewModels;
using System.Runtime.CompilerServices;

namespace NovelReader.UI.Common.Pages
{
    public partial class Login
    {
        [Inject]
        private LoginViewModel m_ViewModel { get; set; } = null!;
        [Inject]
        private IToastService m_ToastService { get; set; } = null!;
        [Inject]
        private NavigationManager m_NavigationManager { get; set; } = null!;

		protected async override Task OnInitializedAsync()
        {
            await m_ToastService.AddMessage(new ToastMessageData
            {
				Type = ToastType.Info,
				Message = "Welcome to the login page!",
			});
		}

        private async Task<string?> OnLogin()
        {
            var errorMsg = await m_ViewModel.Login();
            if (errorMsg == null)
            {
                m_NavigationManager.NavigateTo("/", true);
            }

            return errorMsg;
        }
    }
}
