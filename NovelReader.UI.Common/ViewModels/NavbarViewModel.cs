using NovelReader.Common.ModelDtos.Tag;
using NovelReader.UI.Common.Services.RequestService;
using System.Net.Http.Json;

namespace NovelReader.UI.Common.ViewModels
{
	public class NavbarViewModel
	{
		private readonly IRequestService m_RequestService;
        private List<TagDto> m_Tags = new List<TagDto>();

        public List<TagDto> Tags
        {
			get => m_Tags;
		}

        public NavbarViewModel(IRequestService requestService)
        {
            m_RequestService = requestService; 
        }

        public async Task<string?> LoadAllTags()
        {
            var response = await m_RequestService.GetHttpClient().GetAsync("tags/alltag");

            if (response.IsSuccessStatusCode)
            {
				m_Tags = await response.Content.ReadFromJsonAsync<List<TagDto>>();
				return null;
			}
			else
            {
				return response.ReasonPhrase;
			}
        }
    }
}
