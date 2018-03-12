using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
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
    public class BotDialog : BaseLuisDialog
    {
        private CustomIntentionsResponses customResponses;
        private Announcer botAnnounces;

        public BotDialog()
        {
            customResponses = new CustomIntentionsResponses();
            botAnnounces = new Announcer();
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
            await customResponses.RespondSmallTalk(context, result.Query);
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
    }
}