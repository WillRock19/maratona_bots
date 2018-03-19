using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TelesBot.Services
{
    [Serializable]
    public class UserInputTranslator
    {
        private string ApiUrl;
        private string subscriptionKey;

        [NonSerialized]
        private HttpClient client;

        public UserInputTranslator()
        {
            ApiUrl = ConfigurationManager.AppSettings["TranslateApiUrl"];
            subscriptionKey = ConfigurationManager.AppSettings["TranslateSubscriptionKey"];

            client = new HttpClient();
            //client.BaseAddress = new Uri(ApiUrl);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        }

        public async Task TranslateToEnglish(string text)
        {
            var translate = new KeyValuePair<string, string>(text, "en-us");
            var response = await client.GetAsync($"{ApiUrl}/Translate?to=en-us&text={WebUtility.UrlEncode("n tô bem n")}");


            var a = 10;
        }
    }
}