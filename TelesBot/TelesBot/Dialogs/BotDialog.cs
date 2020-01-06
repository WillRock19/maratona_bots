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
using TelesBot.Interfaces;

namespace TelesBot.Dialogs
{
    [Serializable]
    public class BotDialog : BaseLuisDialog
    {
        private SimpleResponsesGenerator customResponses;
        private IAnnouncer announcer;
        private IDialogFactory dialogFactory;
        private IPromptDialogGenerator promptGenerator;

        public BotDialog(IAnnouncer announcer, IDialogFactory dialogFactory, IPromptDialogGenerator promptGenerator)
        {
            customResponses = new SimpleResponsesGenerator();

            this.announcer = announcer;
            this.dialogFactory = dialogFactory;
            this.promptGenerator = promptGenerator;
        }

        [LuisIntent("Ajuda")]
        public async Task Ajuda(IDialogContext context, LuisResult result)
        {
            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>()
            {
                announcer.Help().ToAttachment()
            };

            await context.PostAsync(message);
        }

        [LuisIntent("Saudacao")]
        public async Task SaudacaoAsync(IDialogContext context, LuisResult result)
        {
            var response = await customResponses.RespondGreeting(result.Query);

            if (response.HasValue())
                await context.PostAsync(response);

            FinalizeContextWithDone(context);
        }

        [LuisIntent("Checar.Emocional.Bot")]
        public async Task ChecandoEmocionalBotAsync(IDialogContext context, LuisResult result)
        {
            var response = customResponses.RespondSmallTalk();

            if(response.HasValue())
                await context.PostAsync(response);

            FinalizeContextWithDone(context);
        }

        [LuisIntent("Emocional.Usuario.Negativo")]
        public async Task EstadoEspiritoUsuarioNegativo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Poxa... não fica assim não! ఠ_ఠ");
            promptGenerator.ConfirmDialog(context, CheckUserWantsAJoke, "Que tal uma piadinha pra melhorar esse humor? ( ・ω・)");
        }

        [LuisIntent("Emocional.Usuario.Positivo")]
        public async Task EstadoEspiritoUsuarioPositivo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Poxa, aí sim ^^. E aí, o que quer fazer hoje?");
        }

        [LuisIntent("ContarPiada")]
        public async Task UsuarioQuerOuvirUmaPiada(IDialogContext context, LuisResult result) => await TellJoke(context);

        private async Task TellJoke(IDialogContext context)
        {
            var formDialog = new FormDialog<ChooseJokes>(new ChooseJokes(), ChooseJokes.BuildForm, FormOptions.PromptInStart);
            context.Call(formDialog, ExecuteAfterForm);
        }

        private async Task ExecuteAfterForm(IDialogContext context, IAwaitable<ChooseJokes> result)
        {
            var data = await result;
            var message = context.MakeMessage();

            message.Text = data.JokeType.GetDescribe();           
            await context.Forward(dialogFactory.Create<JokeDialog>(), ExecuteAfterJokeDialog, message, CancellationToken.None);
        }

        private async Task ExecuteAfterJokeDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var confirmMessage = (await result).ToString();
                promptGenerator.ConfirmDialog(context, CheckUserWantsAJoke, confirmMessage, attemptsAllowed: 2);
            }
            catch(Exception e)
            {
                await context.PostAsync(e.Message);
                context.Wait(MessageReceived);
            }
        }

        private async Task CheckUserWantsAJoke(IDialogContext context, IAwaitable<bool> result)
        {
            var wishAJoke = await result;

            if (!wishAJoke)
            {
                await SendConversationEndMessage(context);
                FinalizeContextWithDone(context);
            }
            else await TellJoke(context);
        }

        private async Task SendConversationEndMessage(IDialogContext context) =>
            await context.PostAsync("Beleza então... qualquer coisa, estarei aqui ^^");
    }
}