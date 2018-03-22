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
        }

        public async Task<Joke> GetJokeByCategory(JokeCategory category) => 
            await GetJoke($"{ApiUrl}?category={category.ToString()}");

        public async Task<Joke> GetJokeBySuperHero(string heroName) => 
            await GetJoke($"{ApiUrl}/SuperHero?heroName={heroName}");

        private async Task<Joke> GetJoke(string urlWithParameters)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ApiUrl);

            var response = await client.GetAsync(urlWithParameters);

            if (!response.IsSuccessStatusCode)
                return new Joke();

            return JsonConvert.DeserializeObject<Joke>(await response.Content.ReadAsStringAsync());
        }
    }
}