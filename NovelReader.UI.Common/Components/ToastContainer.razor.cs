using Microsoft.AspNetCore.Components;
using NovelReader.UI.Common.Services.ToastService;

namespace NovelReader.UI.Common.Components
{
    public partial class ToastContainer : ComponentBase
    {
        [Inject]
        private IToastService m_ToastService { get; set; } = null!;

        private List<ToastMessageData> m_Messages = new List<ToastMessageData>();

		protected async override Task OnInitializedAsync()
        {
            await m_ToastService.SetAddMessageCallback(OnAddMessage);
        }

        private Task OnAddMessage(ToastMessageData messageData)
        {
            m_Messages.Add(messageData);

            Task.Delay(messageData.Duration).ContinueWith(_ => RemoveMessage(messageData));

            StateHasChanged();
            return Task.CompletedTask;
        }

        private void RemoveMessage(ToastMessageData messageData)
        {
            if (m_Messages.Contains(messageData))
            {
				m_Messages.Remove(messageData);
				StateHasChanged();
            }
        }
    }
}
