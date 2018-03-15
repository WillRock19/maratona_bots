using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using TelesBot.Enums;
using TelesBot.Extensions;
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

        public async Task<Joke> GetJokeByCategory(JokeCategory category)
        {
            var urlWithParameters = $"{ApiUrl}?category={category.ToString()}";
            var response = await client.GetAsync(urlWithParameters);

            if (!response.IsSuccessStatusCode)
                return new Joke();

            return JsonConvert.DeserializeObject<Joke>(await response.Content.ReadAsStringAsync());
        }
    }
}