using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using TelesBot.Enums;
using TelesBot.Interfaces;
using TelesBot.Model;

namespace TelesBot.Services
{
    public class JokeSearcher : IJokeSearcher
    {
        private string ApiUrl;
        private HttpClient client;

        public JokeSearcher(HttpClient httpClient)
        {
            ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            client = httpClient;
            client.BaseAddress = new Uri(ApiUrl);
        }

        public async Task<Joke> GetJokeByCategory(JokeCategory category) => 
            await GetJoke($"{ApiUrl}?category={category.ToString()}");

        public async Task<Joke> GetJokeBySuperHero(string heroName) => 
            await GetJoke($"{ApiUrl}/SuperHero?heroName={heroName}");

        private async Task<Joke> GetJoke(string urlWithParameters)
        {
            var response = await client.GetAsync(urlWithParameters);

            if (!response.IsSuccessStatusCode)
                return new Joke();

            return JsonConvert.DeserializeObject<Joke>(await response.Content.ReadAsStringAsync());
        }
    }
}