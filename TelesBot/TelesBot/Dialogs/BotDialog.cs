using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelesBot.CustomResponses;

namespace TelesBot.Dialogs
{
    [Serializable]
    public class BotDialog : LuisDialog<object>
    {
        private CustomIntentionsResponses customResponses;
        private Announcer botAnnounces;

        public BotDialog(ILuisService service) : base(service)
        {
            customResponses = new CustomIntentionsResponses();
            botAnnounces = new Announcer();
        }

        [LuisIntent("")]
        public async Task IntencaoNaoReconhecida(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(@"**( ͡° ͜ʖ ͡°)** - Desculpe, mas não entendi o que você quis dizer. \n\n
                                    Lembre-se que sou um bot e meu conhecimento é limitado.");
            context.Done<string>(null);
        }

        [LuisIntent("Ajuda")]
        public async Task Ajuda(IDialogContext context, LuisResult result)
        {
            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>();

            message.Attachments.Add(botAnnounces.Help().ToAttachment());
            await context.PostAsync(message);
        }

        [LuisIntent("None")]
        public async Task NoneAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Não entendi **(ಥ﹏ಥ)**... Pode tentar repetir com outras palavras?");
            context.Done<string>(null);
        }

        [LuisIntent("Cumprimentar")]
        public async Task CumprimentoAsync(IDialogContext context, LuisResult result)
        {
            await customResponses.RespondGreeting(context, result.Query);
            context.Done<string>(null);
        }

        [LuisIntent("trocar-ideia")]
        public async Task TrocarIdeiaAsync(IDialogContext context, LuisResult result)
        {
            await customResponses.RespondWithQnaMaker(context, result.Query);
            context.Done<string>(null);
        }

        [LuisIntent("Emocional.Usuario")]
        public async Task EstadoEspiritoUsuario(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Estou me sentindo emotivo... **(ಥ﹏ಥ)**");
        }
    }
}