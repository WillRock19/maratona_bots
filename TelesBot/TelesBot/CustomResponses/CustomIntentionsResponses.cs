using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;
using TelesBot.Helpers;

namespace TelesBot.CustomResponses
{
    [Serializable]
    public class CustomIntentionsResponses
    {
        private int greetingsAnswered;
        private int smallTalkAnswered;
        private QnaMakerHelper qnaMaker;

        public CustomIntentionsResponses()
        {
            qnaMaker = new QnaMakerHelper();
            greetingsAnswered = 0;
            smallTalkAnswered = 0;
        }


        public async Task RespondWithQnaMaker(IDialogContext context, string query) => 
            await SearchAnswerInQnaMaker(context, query);

        public async Task RespondGreeting(IDialogContext context, string greeting)
        {
            switch (greetingsAnswered)
            {
                case 0:
                    await SearchAnswerInQnaMaker(context, greeting);
                    break;
                case 1:
                    await AnswerUser(context, "Hã... é... 'Oi' de novo... **( ͡° ͜ʖ ͡°)**");
                    break;
                case 2:
                    await AnswerUser(context, "Ok... agora você só quer me provocar, né? ¬¬");
                    break;
                case 3:
                    await AnswerUser(context, "Ei, já brincou do jogo do silêncio?");
                    break;
            }
            IncreaseGreetingsAnswered();
        }

        public async Task RespondSmallTalk(IDialogContext context, string userText)
        {
            if (greetingsAnswered > 0)
            {
                await AnswerUser(context, "Ahhh... agora resolveu conversar direito? **( ͡° ͜ʖ ͡°)**");
                await AnswerUser(context, "Tô bem sim... (vamos ver se continuo assim >.>). E você? ^^");
                ClearGreetingsAnswered();
            }
            else
            {
                switch (smallTalkAnswered)
                {
                    case 0:
                        await SearchAnswerInQnaMaker(context, userText);
                        break;
                    case 1:
                        await AnswerUser(context, "Eu... tudo bem de novo... **( ͡° ͜ʖ ͡°)**");
                        break;
                    case 2:
                        await AnswerUser(context, "Ah... gosta de ser espertalão, né? ¬¬");
                        break;
                    case 3:
                        await AnswerUser(context, "Já brincou do jogo do silêncio?");
                        break;
                }
                IncreaseSmallTalksAnswered();
            }
        }

        private async Task SearchAnswerInQnaMaker(IDialogContext context, string query)
        {
            var result = await qnaMaker.SearchForHighScoreAnswer(query);
            await AnswerUser(context, result);
        }

        private async Task AnswerUser(IDialogContext context, string answer) => await context.PostAsync(answer);

        private void IncreaseGreetingsAnswered() => 
            greetingsAnswered++;

        private void IncreaseSmallTalksAnswered() =>
            smallTalkAnswered++;

        private void ClearGreetingsAnswered() =>
            greetingsAnswered = 0;

        private void ClearSmallTalkAnswered() =>
            smallTalkAnswered++;


    }
}