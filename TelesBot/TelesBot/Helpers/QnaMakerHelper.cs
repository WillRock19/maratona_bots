using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TelesBot.Helpers
{
    [Serializable]
    public class QnaMakerHelper
    {
        private string qnaKnowledgeBaseId { get; set; }
        private string qnaSubscriptionKey { get; set; }
        private string defaultNotFindMessage { get; set; }
        private double precisionScore { get; set; }
        private QnAMakerService service { get; set; }

        public QnaMakerHelper()
        {
            qnaKnowledgeBaseId = ConfigurationManager.AppSettings["QnaSubscriptionKey"];
            qnaSubscriptionKey = ConfigurationManager.AppSettings["QnaKnowledgeBaseId"];
            defaultNotFindMessage = @"Eita... não sei responder **(ಥ﹏ಥ)**. Não quer tentar usar outras palavras?";
            precisionScore = 0.40;

            CreateService();
        }

        public async Task<string> SearchForHighScoreAnswer(string query)
        {
            var text = WorkBrazilianHelloVariations(query);
            var qnaResult = await service.QueryServiceAsync(text);

            if (!qnaResult.Answers.Any())
                return defaultNotFindMessage;

            var higherScoreResult = FindAnswerWithHigherScore(qnaResult.Answers);
            return higherScoreResult.Answer;
        }

        private void CreateService()
        {
            var attributes = new QnAMakerAttribute(qnaKnowledgeBaseId, qnaSubscriptionKey, defaultNotFindMessage, precisionScore);
            service = new QnAMakerService(attributes);
        }

        private QnAMakerResult FindAnswerWithHigherScore(IEnumerable<QnAMakerResult> answers) =>
            answers.Aggregate((previousAnswer, currentAnswer) => 
                previousAnswer.Score > currentAnswer.Score ? previousAnswer : currentAnswer);

        private string WorkBrazilianHelloVariations(string hello) =>
            Regex.Replace(Regex.Replace(Regex.Replace(hello, "o+", "o"), "i+", "i"), "e+", "e");
    }
}