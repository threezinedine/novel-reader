using Microsoft.AspNetCore.Components;

namespace NovelReader.UI.Common.Components
{
	public partial class Column
	{
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
		[Parameter]
		public string Class { get; set; } = string.Empty;
	}
}
