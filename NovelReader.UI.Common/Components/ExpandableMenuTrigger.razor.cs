using Microsoft.AspNetCore.Components;

namespace NovelReader.UI.Common.Components
{
    public partial class ExpandableMenuTrigger
    {
        [CascadingParameter]
        public ExpandableMenu Parent { get; set; } = null!;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
