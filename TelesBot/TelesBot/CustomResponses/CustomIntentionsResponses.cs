using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Linq;
using System.Threading.Tasks;
using TelesBot.Helpers;

namespace TelesBot.CustomResponses
{
    [Serializable]
    public class CustomIntentionsResponses
    {
        private int greetingsAnswered;
        private int smallTalkAnswered;
        private bool userTriedToBeTroll;

        private BotCustomResponses customResponses;

        private QnaMakerHelper qnaMaker;

        public CustomIntentionsResponses()
        {
            qnaMaker = new QnaMakerHelper();
            customResponses = new BotCustomResponses();

            userTriedToBeTroll = false;
            greetingsAnswered = 0;
            smallTalkAnswered = 0;
        }

        public async Task RespondWithQnaMaker(IDialogContext context, string query) => 
            await SearchAnswerInQnaMaker(query);

        public async Task<string> RespondGreeting(string greeting)
        {
            var response = !userTriedToBeTroll ? 
                            await respondGreetingsPatiently(greeting) : 
                            respondGreetingsImpatient();

            IncreaseGreetingsAnswered();
            CheckIfBotLosePatienceWithUser();

            return response;
        }

        public string RespondSmallTalk()
        {
            var response = !userTriedToBeTroll ?
                            respondSmallTalkPatiently() :
                            respondSmallTalkImpatient();

            ReduceGreetingsAnsweredToOne();
            IncreaseSmallTalksAnswered();
            return response;
        }

        public string RespondUserPositiveStateOfMind()
        {
            return string.Empty;
        }

        public string RespondUserNegativeStateOfMind()
        {
            return "Poxa... não fica assim não! ఠ_ఠ";
        }

        private async Task<string> respondGreetingsPatiently(string greetings)
        {
            if (!firstGreetingAnswered())
                return customResponses.Greetings()
                        .ElementAtOrDefault(greetingsAnswered) 
                        ?? string.Empty;

            return await SearchAnswerInQnaMaker(greetings);
        }

        private string respondGreetingsImpatient() => customResponses
                                                        .GreetingsAfterTrolling()
                                                        .ElementAtOrDefault(greetingsAnswered) 
                                                        ?? string.Empty;

        private string respondSmallTalkPatiently() => 
                    customResponses.SmallTalk()
                                   .ElementAtOrDefault(smallTalkAnswered) 
                                   ?? string.Empty;

        private string respondSmallTalkImpatient() =>
                    customResponses.SmallTalkAfterTrolling()
                                   .ElementAtOrDefault(smallTalkAnswered)
                                   ?? string.Empty;

        private async Task<string> SearchAnswerInQnaMaker(string query) => 
            await qnaMaker.SearchForHighScoreAnswer(query);

        private bool firstGreetingAnswered() => 
            greetingsAnswered == 0;

        private void IncreaseGreetingsAnswered() => 
            greetingsAnswered++;

        private void IncreaseSmallTalksAnswered() =>
            smallTalkAnswered++;

        private void ReduceGreetingsAnsweredToOne()
        {
            if(greetingsAnswered > 1 && smallTalkAnswered < 2)
                greetingsAnswered = 1;
        }

        private void CheckIfBotLosePatienceWithUser() => 
            userTriedToBeTroll = greetingsAnswered > 3;
    }
}