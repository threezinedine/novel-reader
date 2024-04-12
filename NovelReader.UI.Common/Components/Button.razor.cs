using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace NovelReader.UI.Common.Components
{
	public partial class Button
	{
		[Parameter]
		public string Title { get; set; } = string.Empty;
		[Parameter]
		public string CommonClass { get; set; } = string.Empty;
		[Parameter]
		public string NonHoverClass { get; set; } = string.Empty;
		[Parameter]
		public string HoverClass { get; set; } = string.Empty;
		[Parameter]
		public string? To { get; set; }
		[Parameter]
		public Action? OnClick { get; set; }

		[Inject]
		private NavigationManager NavigationManager { get; set; } = null!;

		private bool m_IsHovered = false;

		private void OnButtonClick()
		{
			if (To != null)
			{
				NavigationManager.NavigateTo(To);
			}
			else
			{
				if (OnClick != null)
				{
					MethodInfo methodInfo = OnClick.Method;
					methodInfo.Invoke(OnClick.Target, null);
				}
			}
		}
	}
}
