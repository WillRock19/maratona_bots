using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using TelesBot.Enums;
using TelesBot.Extensions;
using TelesBot.Helpers;
using TelesBot.Services;

namespace TelesBot.Dialogs
{
    [Serializable]
        
    public class JokeDialog : BaseLuisDialog
    {
        private JokeSearcher jokeSearcher;

        private CardGeneratorHelper cardGenerator;
        private Model.Joke jokeFinded;

        public JokeDialog()
        {
            jokeSearcher = new JokeSearcher();
            cardGenerator = new CardGeneratorHelper();
        }

        [LuisIntent("ContarPiada.Simples")]
        public async Task ContarUmaPiadaSimples(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Certo, deixa eu pensar em alguma... (◔.◔)");
            await SendIsTypingMessage(context);

            var category = result.Query.ToEnumByDescribe<JokeCategory>();
            await GetSimpleJokeAndMakePun(context, category);
        }

        [LuisIntent("ContarPiada.Herois")]
        public async Task ContarUmaPiadaDeSuperHeroi(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Beleza então. Manda o símbolo de um desses super-heróis que eu conto a piada:" +
                    "\n\n\r\t" +
                    "* **Batman**;" + 
                    "* **Demolidor**;" +
                    "* **Superman**;");

            context.Wait(GetImageAndMakeHeroJoke);
        }

        private async Task GetImageAndMakeHeroJoke(IDialogContext context, IAwaitable<object> result)
        {

        }

        private async Task GetSimpleJokeAndMakePun(IDialogContext context, JokeCategory category)
        {
            jokeFinded = await jokeSearcher.GetJokeByCategory(category);

            if (jokeFinded.Exists())
            {
                await context.PostAsync(jokeFinded.Introduction);
                context.Wait(ListenUserResponseAndFinishJoke);
            }
            else
            {
                await context.PostAsync("Não consegui pensar em nada x.x");
                await context.PostAsync("Me desculpa...");
                FinalizeContextWithFail(context);
            }
        }

        private async Task ListenUserResponseAndFinishJoke(IDialogContext context, IAwaitable<object> result)
        {
            var userResponse = await result;
            var message = context.MakeMessage();

            message.Text = $"**'{jokeFinded.Conclusion}'**";
            message.Attachments.Add(cardGenerator.SimpleAnimationCard("", "https://media1.tenor.com/images/2e678e259bc84c7729a3465ce79c4929/tenor.gif?itemid=5908888"));

            await context.PostAsync(message);
            FinalizeContextWithSuccess(context);
        }
    }
}