using Microsoft.AspNetCore.Components;

namespace NovelReader.UI.Common.Components
{
    public partial class ExpandableMenu : ComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool ClickedTrigger { get; set; } = false;

        private bool m_IsExpanded = false;

        public bool IsExpanded
        {
            get => m_IsExpanded;
        }

        public void OnMouseIn()
        {
            if (!ClickedTrigger)
            {
                m_IsExpanded = true;
            }
        }

        public void OnMouseOut()
        {
            if (!ClickedTrigger)
            {
                m_IsExpanded = false;
            }
        }

        public void OnClick()
        {
            if (ClickedTrigger)
            {
                m_IsExpanded = !m_IsExpanded;
            }
        }
    }
}
