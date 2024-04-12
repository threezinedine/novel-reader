using Microsoft.AspNetCore.Components;

namespace NovelReader.UI.Common.Components
{
	public partial class FormSubmit
	{
		[CascadingParameter]
		public Form? ParentForm { get; set; }

		private void OnClick()
		{
			if (ParentForm != null)
			{
				ParentForm.Submit();
			}
		}
	}
}
