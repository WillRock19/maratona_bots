using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using TelesBot.Enums;
using TelesBot.Extensions;
using TelesBot.Services;

namespace TelesBot.Dialogs
{
    [Serializable]
        
    public class JokeDialog : BaseLuisDialog
    {
        private JokeSearcher jokeSearcher;
        private Model.Joke jokeFinded;

        public JokeDialog()
        {
            jokeSearcher = new JokeSearcher();
        }

        [LuisIntent("ContarPiada.Simples")]
        public async Task ContarUmaPiadaDeTiozao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Certo, deixa eu pensar em alguma... (◔.◔)");

            var category = result.Query.ToEnumWithThisDescribe<JokeCategory>();
            jokeFinded = await jokeSearcher.GetJokeByCategory(category);

            if (jokeFinded.Exists())
            {
                await context.PostAsync(jokeFinded.Introduction);
                context.Wait(GetUserResponseAndFinishJoke);
            }
            else
            {
                await context.PostAsync("Cara... não consegui pensar em nada x.x");
                await context.PostAsync("Me desculpa...");

                context.Done(context.MakeMessage());
            }
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

        private async Task GetUserResponseAndFinishJoke(IDialogContext context, IAwaitable<object> result)
        {
            var userResponse = await result;
            await context.PostAsync(jokeFinded.Conclusion);

            FinalizeContextWithSuccess(context);
        }
    }
}