using NovelReader.UI.Common.Components;

namespace NovelReader.UI.Common.Services.ToastService
{
    public enum ToastType
    {
        Success,
        Error,
        Info,
        Warning,
    }
    public class ToastMessageData
    {
        public ToastType Type { get; set; } = ToastType.Info;
        public string Message { get; set; } = string.Empty;
        public int Duration { get; set; } = 1000;
    }

    public interface IToastService
    {
        public Task AddMessage(ToastMessageData messageData);
        public Task SetAddMessageCallback(Func<ToastMessageData, Task> callback);
    }
}
