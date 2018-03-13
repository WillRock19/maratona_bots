using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using TelesBot.Services;

namespace TelesBot.Dialogs
{
    [Serializable]
        
    public class JokeDialog : BaseLuisDialog
    {
        private JokeSearcher jokeSearcher;

        public JokeDialog()
        {
            jokeSearcher = new JokeSearcher();
        }

        [LuisIntent("ContarPiada.Simples")]
        public async Task ContarUmaPiadaDeTiozao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Certo, deixa eu pensar em alguma... (◔.◔)");

            //jokeSearcher.GetJokeByCategory();
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
    }
}