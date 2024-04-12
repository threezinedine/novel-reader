using Microsoft.AspNetCore.Components;
using NovelReader.UI.Common.Services.ToastService;

namespace NovelReader.UI.Common.Components
{
	public partial class Form : ComponentBase
	{
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
		[Parameter]
		public Func<Task<string?>>? OnSubmit { get; set; }
		[Inject]
		private IToastService m_ToastService { get; set; } = null!;
		
		private List<string> m_FieldErrors = new List<string>();

		private bool m_CanSubmit
		{
			get => m_FieldErrors.Count == 0;
		}

		public void AddNewField(string fieldName)
		{
			if (!m_FieldErrors.Contains(fieldName))
			{
				m_FieldErrors.Add(fieldName);
			}
		}

		public void RemoveField(string fieldName)
		{
			if (m_FieldErrors.Contains(fieldName))
			{
				m_FieldErrors.Remove(fieldName);
			}
		}

		public async void Submit()
		{
			string toastErrorMessage = "Error in field(s): ";
			foreach (var error in m_FieldErrors)
			{
                toastErrorMessage += error + ", ";
			}

			if (OnSubmit != null)
			{
				if (m_CanSubmit)
				{
                    var errorMessage = await OnSubmit();

                    if (errorMessage != null)
                    {
						await m_ToastService.AddMessage(new ToastMessageData
						{
							Type = ToastType.Error,
							Message = errorMessage,
						});
                    }
				}
				else
				{
					await m_ToastService.AddMessage(new ToastMessageData
					{
                        Type = ToastType.Error,
                        Message = toastErrorMessage
                    });
				}
			}
		}
	}
}
