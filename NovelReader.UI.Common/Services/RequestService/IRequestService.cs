using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReader.UI.Common.Services.RequestService
{
    public interface IRequestService
    {
        public Task LoadToken();
        public HttpClient GetHttpClient();
        public Task SaveToken(string token);
    }
}
