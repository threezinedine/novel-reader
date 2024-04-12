using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReader.UI.Common.Components
{
    public partial class ExpandableMenuContent
    {
        [CascadingParameter]
        public ExpandableMenu Parent { get; set; } = null!;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
