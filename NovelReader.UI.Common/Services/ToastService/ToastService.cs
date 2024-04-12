
using NovelReader.UI.Common.Components;

namespace NovelReader.UI.Common.Services.ToastService
{
    public class ToastService : IToastService
    {
        private Func<ToastMessageData, Task>? m_AddMessageCallback;
        public async Task AddMessage(ToastMessageData messageData)
        {
            if (m_AddMessageCallback != null)
            {
                await m_AddMessageCallback(messageData);
            }
        }

        public Task SetAddMessageCallback(Func<ToastMessageData, Task> callback)
        {
            m_AddMessageCallback = callback;
            return Task.CompletedTask;
        }
    }
}
