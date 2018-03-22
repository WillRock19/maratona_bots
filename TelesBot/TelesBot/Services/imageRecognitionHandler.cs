using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TelesBot.Model;

namespace TelesBot.Services
{
    [Serializable]
    public class ImageRecognitionHandler
    {
        private string predictionUrl;
        private string predictionKey;

        [NonSerialized]
        private HttpClient client;

        public ImageRecognitionHandler()
        {
            predictionUrl = ConfigurationManager.AppSettings["ImagePredictionUrl"];
            predictionKey = ConfigurationManager.AppSettings["ImagePredictionKey"];
        }

        public async Task<string> getImageTags(Attachment attachment)
        {
            client = new HttpClient();
            var image = await getImage(attachment.ContentUrl);

            if (image.IsSuccessStatusCode)
            {
                var analysis = await getImageAnalysis(client, image.Content);

                if (!analysis.IsSuccessStatusCode)
                    return string.Empty;

                return await findMostLikelyImageTag(analysis);
            }

            client.Dispose();
            return string.Empty;
        }

        private async Task<HttpResponseMessage> getImage(string url) =>
            await client.GetAsync(url);

        private async Task<HttpResponseMessage> getImageAnalysis(HttpClient client, HttpContent image)
        {
            client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);
            return await client.PostAsync(predictionUrl, image);
        }

        private async Task<string> findMostLikelyImageTag(HttpResponseMessage imageAnalysis)
        {
            var result = JsonConvert.DeserializeObject<ImageAnalysisResult>(await imageAnalysis.Content.ReadAsStringAsync());
            var mostProbableTag = result.Predictions.FirstOrDefault();

            return mostProbableTag.Probability > 0.5 ? mostProbableTag.Tag : string.Empty;
        }
    }
}