using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelesBot.CustomResponses;
using TelesBot.Forms;

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
            await context.PostAsync("**( ͡° ͜ʖ ͡°)** - Desculpe, mas não entendi o que você quis dizer");
            await context.PostAsync("Lembre-se que sou um bot e meu conhecimento é limitado.");
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

        [LuisIntent("Cumprimentar")]
        public async Task CumprimentoAsync(IDialogContext context, LuisResult result)
        {
            await customResponses.RespondGreeting(context, result.Query);
            context.Done<string>(null);
        }

        [LuisIntent("Usuario.Checando.Emocional.Bot")]
        public async Task TrocarIdeiaAsync(IDialogContext context, LuisResult result)
        {
            await customResponses.RespondWithQnaMaker(context, result.Query);
            context.Done<string>(null);
        }

        [LuisIntent("Usuario.Informando.Emocional")]
        public async Task EstadoEspiritoUsuario(IDialogContext context, LuisResult result)
        {
            await customResponses.RespondWithQnaMaker(context, result.Query);
            context.Done<string>(null);
        }

        [LuisIntent("ContarPiada")]
        public async Task ContarUmaPiada(IDialogContext context, LuisResult result)
        {
            var formDialog = new FormDialog<ChooseJokes>(new ChooseJokes(), ChooseJokes.BuildForm, FormOptions.PromptInStart);
            context.Call(formDialog, null);
        }

        [LuisIntent("ContarPiada.Tiozao")]
        public async Task ContarUmaPiadaDeTiozao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Certo, deixa eu pensar em alguma... (◔.◔)");
        }

        [LuisIntent("ContarPiada.HumorNegro")]
        public async Task ContarUmaPiadaDeHumorNegro(IDialogContext context, LuisResult result)
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