using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
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
            jokeFinded = await jokeSearcher.GetJokeByCategory(result.Query);

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

        [LuisIntent("ContarPiada.SuperHeroi")]
        public async Task ContarUmaPiadaDeSuperHeroi(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Beleza então. Manda uma foto de super-herói que eu conto a piada! ^^");
            await context.PostAsync("Olha os que eu conheço: " +
                    "* **Batman**;" + 
                    "* **Demolidor**;" +
                    "* **Superman**;");

            //context.Wait(MakeSuperHeroeJoke);

        }

        private async Task MakeSuperHeroeJoke(IDialogContext context, LuisResult result)
        {

        }

        private async Task GetUserResponseAndFinishJoke(IDialogContext context, IAwaitable<object> result)
        {
            var userResponse = await result;
            await context.PostAsync(jokeFinded.Conclusion);

            context.Done<string>(null);
        }
    }
}