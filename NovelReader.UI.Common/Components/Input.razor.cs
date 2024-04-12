using Microsoft.AspNetCore.Components;
using NovelReader.UI.Common.Services.IdGeneratorService;
using System.Net.Http.Headers;

namespace NovelReader.UI.Common.Components
{
    public enum InputType
    {
        Text,
        Password,
    }

    public partial class Input
    {
        [CascadingParameter]
        public Form ParentForm { get; set; } = null!;
        [Parameter]
        public string Value { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public string Placeholder { get; set; } = string.Empty;
        [Parameter]
        public InputType Type { get; set; } = InputType.Text;
        [Parameter]
        public Func<string, string?>? Validate { get; set; }
        [Inject]
        public IdGeneratorService IdGeneratorService { get; set; } = null!;

        private string Id { get; set; } = string.Empty;
        private string? m_ErrorMessage;

        private string CurrentValue
        {
            get => Value;
            set
            {
                Value = value;
                ValueChanged.InvokeAsync(value);
                
                if (Validate == null)
                {
					ParentForm.RemoveField(Label);
                }
                else
                {
                    m_ErrorMessage = Validate(value);

                    if (m_ErrorMessage != null)
                    {
						ParentForm.AddNewField(Label);
                    }
                    else
                    {
                        ParentForm.RemoveField(Label);
                    }
                }
            }
        }

		protected override Task OnInitializedAsync()
		{
            if (Id == string.Empty)
            {
				Id = IdGeneratorService.GenerateId(10);
            }

            ParentForm.AddNewField(Label);
            return Task.CompletedTask;
		}

        private string GetInputType()
        {
            switch (Type)
            {
				case InputType.Text:
					return "text";
				case InputType.Password:
					return "password";
				default:
					return "text";
			}
        }
	}
}
