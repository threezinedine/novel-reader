
using NovelReader.UI.Common.Services.LocalStorageService;
using System.Net.Http.Headers;

namespace NovelReader.UI.Common.Services.RequestService
{
	public class RequestService : IRequestService
	{
		private readonly HttpClient m_HttpClient;
		private readonly IStorageService m_StorageService;
		private readonly string m_TokenKey = "token";

        public RequestService(HttpClient httpClient,
								IStorageService storageService)
        {
			m_HttpClient = httpClient; 
			m_StorageService = storageService;
        }
        public HttpClient GetHttpClient()
		{
			return m_HttpClient;
		}

		public async Task LoadToken()
		{
			var token = await m_StorageService.Get<string>(m_TokenKey, string.Empty);

            if (token != string.Empty)
            {
				m_HttpClient.DefaultRequestHeaders.Authorization
					= new AuthenticationHeaderValue("Bearer", token);
            }
        }

		public async Task SaveToken(string token)
		{
			await m_StorageService.Set(m_TokenKey, token);
		}
	}
}
