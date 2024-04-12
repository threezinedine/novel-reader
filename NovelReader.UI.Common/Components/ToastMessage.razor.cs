using Microsoft.AspNetCore.Components;
using NovelReader.UI.Common.Services.ToastService;

namespace NovelReader.UI.Common.Components
{
    public partial class ToastMessage : ComponentBase
    {
        [Parameter]
        public ToastMessageData Data { get; set; } = new ToastMessageData();
        [Parameter]
        public Action OnRemove { get; set; } = null!;

        private string GetColorClass()
        {
            switch (Data.Type)
            {
				case ToastType.Success:
					return "text-green-500";
				case ToastType.Error:
					return "text-red-500";
				case ToastType.Warning:
					return "text-yellow-500";
				default:
					return "text-blue-500";
			}
        }

        private string GetIconData()
        {
            switch (Data.Type)
            {
                case ToastType.Success:
					return "fa-check-circle";
                case ToastType.Error:
                    return "fa-exclamation-circle";
                case ToastType.Warning:
                    return "fa-exclamation-triangle";
                default:
                    return "fa-info-circle";
            }
        }
    }
}
