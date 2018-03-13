using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TelesBot.CustomResponses;
using TelesBot.Extensions;
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
        }

        [LuisIntent("Usuario.Informando.Emocional")]
        public async Task EstadoEspiritoUsuario(IDialogContext context, LuisResult result)
        {
            await customResponses.RespondWithQnaMaker(context, result.Query);
        }

        [LuisIntent("ContarPiada")]
        public async Task ContarUmaPiada(IDialogContext context, LuisResult result)
        {
            var formDialog = new FormDialog<ChooseJokes>(new ChooseJokes(), ChooseJokes.BuildForm, FormOptions.PromptInStart);
            context.Call(formDialog, ExecuteAfterForm);
        }

        private async Task ExecuteAfterForm(IDialogContext context, IAwaitable<ChooseJokes> result)
        {
            var data = await result;
            var message = context.MakeMessage();

            message.Text = data.JokeType.GetDescribe();           
            await context.Forward(new JokeDialog(), ResumeAfterJokeDialog, message, CancellationToken.None);
        }

        private async Task ResumeAfterJokeDialog(IDialogContext context, IAwaitable<object> result)
        {
            var ishi = await result;
        }
    }
}