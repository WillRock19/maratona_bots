using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace TelesBot.Dialogs
{
    [Serializable]
    public class BaseLuisDialog : LuisDialog<object>
    {
        public BaseLuisDialog() : base(CreateNewService()) { }

        [LuisIntent("None")]
        public async Task NenhumaIntencao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Eiiita... eu não entendi o que você quis dizer **(╥_╥)**");
            await context.PostAsync("Lembre-se que sou um bot e meu conhecimento é limitado. (͡๏̯ ͡๏)");
            context.Wait(base.MessageReceived);
        }

        [LuisIntent("")]
        public async Task IntencaoNaoReconhecida(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Desculpe, viajei por um instante ✖_✖");
            await context.PostAsync("Pode repetir com outras palavras? ლ(╹◡╹ლ)");
            context.Wait(base.MessageReceived);
        }

        private static ILuisService CreateNewService()
        {
            var luisModel = new LuisModelAttribute(ConfigurationManager.AppSettings["LuisModelId"], ConfigurationManager.AppSettings["LuisSubscriptionKey"]);
            return new LuisService(luisModel);
        }
    }
}