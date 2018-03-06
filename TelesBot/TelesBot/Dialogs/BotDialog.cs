//using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace TelesBot.Dialogs
{
    [Serializable]
    [LuisModel("9abad72c-e333-4e14-bf3a-cab442a632d5", "bc1e37ee93a8408db7f30a120ff6e375")]
    public class BotDialog : LuisDialog<object>
    {
        //public BotDialog(ILuisService service) : base(service) { }

        [LuisIntent("")]
        public async Task IntencaoNaoReconhecida(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(@"**( ͡° ͜ʖ ͡°)** - Desculpe, mas não entendi o que você quis dizer. \n
                                    Lembre-se que sou um bot e meu conhecimento é limitado.");
            context.Done<string>(null);
        }


        [LuisIntent("None")]
        public async Task NoneAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(@"Vishe **(ಥ﹏ಥ)**... \n
                                Não entendi o que você disse, me desculpa **(ಥ﹏ಥ)** \n
                                Pode tentar repetir com outras palavras?");

            context.Done<string>(null);
        }

        [LuisIntent("Cumprimentar")]
        public async Task CumprimentoAsync(IDialogContext context, LuisResult result)
        {
            //var attributes = QnaMakerAttributes


            await context.PostAsync("Você falou comigo? **(ಥ﹏ಥ)**");
        }

        [LuisIntent("trocar-ideia")]
        public async Task TrocarIdeiaAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Não quero falar agora **(▀̿Ĺ̯▀̿ ̿)**");
        }
    }
}