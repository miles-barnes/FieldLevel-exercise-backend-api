using Fieldlevel.Models;
using Fieldlevel.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace Fieldlevel.Services
{
    public class RemotePostProvider : IPostProvider, IDisposable
    {
        private readonly RemoteServiceSettings _settings;
        private readonly HttpClient _httpClient;

        public RemotePostProvider(RemoteServiceSettings settings)
        {
            _settings = settings;
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public IEnumerable<Post> GetPosts()
        {
            using (var task = _httpClient.GetAsync(_settings.PostsUrl))
            using (var response = task.Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<Post[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                    return null;
            }
        }
    }
}