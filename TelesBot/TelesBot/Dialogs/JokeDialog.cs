using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace TelesBot.Dialogs
{
    [Serializable]
        
    public class JokeDialog : BaseLuisDialog
    {
        public JokeDialog() { }

        [LuisIntent("ContarPiada.Simples")]
        public async Task ContarUmaPiadaDeTiozao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Certo, deixa eu pensar em alguma... (◔.◔)");
        }

        [LuisIntent("ContarPiada.SuperHeroi")]
        public async Task ContarUmaPiadaDeSuperHeroi(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Certo, deixa eu pensar em alguma... (◔.◔)");
        }
    }
}