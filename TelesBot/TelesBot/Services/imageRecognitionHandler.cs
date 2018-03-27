using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TelesBot.Interfaces;
using TelesBot.Model;

namespace TelesBot.Services
{
    public class ImageRecognitionHandler : IImageRecognitionHandler
    {
        private string predictionUrl;
        private string predictionKey;
        private HttpClient client;

        public ImageRecognitionHandler(HttpClient httpClient)
        {
            predictionUrl = ConfigurationManager.AppSettings["ImagePredictionUrl"];
            predictionKey = ConfigurationManager.AppSettings["ImagePredictionKey"];

            client = httpClient;
        }

        public async Task<string> getImageTags(Attachment attachment)
        {
            var image = await getImage(attachment.ContentUrl);

            if (image.IsSuccessStatusCode)
            {
                var analysis = await getImageAnalysis(image.Content);

                if (!analysis.IsSuccessStatusCode)
                    return string.Empty;

                return await findMostLikelyImageTag(analysis);
            }

            return string.Empty;
        }

        private async Task<HttpResponseMessage> getImage(string url) =>
            await client.GetAsync(url);

        private async Task<HttpResponseMessage> getImageAnalysis(HttpContent image)
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