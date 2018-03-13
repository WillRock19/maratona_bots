using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using TelesBot.Model;

namespace TelesBot.Services
{
    [Serializable]
    public class JokeSearcher
    {
        private string ApiUrl;

        [NonSerialized]
        private HttpClient client;

        public JokeSearcher()
        {
            ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];

            client = new HttpClient();
            client.BaseAddress = new Uri(ApiUrl);
        }

        public async Task<Joke> GetJokeByCategory(string category)
        {
            var urlWithParameters = $"{ApiUrl}?category={category}";
            var response = await client.GetAsync(urlWithParameters);

            if (!response.IsSuccessStatusCode)
                return new Joke();

            var joke = JsonConvert.DeserializeObject<Joke>(await response.Content.ReadAsAsync<string>());
            return joke;
        }
    }
}