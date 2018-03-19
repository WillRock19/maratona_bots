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
using TelesBot.Helpers;

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

        [LuisIntent("Saudacao")]
        public async Task SaudacaoAsync(IDialogContext context, LuisResult result)
        {
            var response = await customResponses.RespondGreeting(result.Query);

            if (!string.IsNullOrWhiteSpace(response))
                await context.PostAsync(response);

            FinalizeContextWithSuccess(context);
        }

        [LuisIntent("Checar.Emocional.Bot")]
        public async Task ChecandoEmocionalBotAsync(IDialogContext context, LuisResult result)
        {
            var response = customResponses.RespondSmallTalk();

            if(!string.IsNullOrWhiteSpace(response))
                await context.PostAsync(response);

            FinalizeContextWithSuccess(context);
        }

        [LuisIntent("Emocional.Usuario.Negativo")]
        public async Task EstadoEspiritoUsuarioNegativo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Poxa... não fica assim não! ఠ_ఠ");
            await OfferUserAJoke(context, "Que tal uma piadinha pra melhorar esse humor? ( ・ω・)");
        }

        [LuisIntent("Emocional.Usuario.Positivo")]
        public async Task EstadoEspiritoUsuarioPositivo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Poxa, aí sim ^^. E aí, o que quer fazer hoje?");
        }

        [LuisIntent("ContarPiada")]
        public async Task UsuarioQuerOuvirUmaPiada(IDialogContext context, LuisResult result) =>
            await TellOneJoke(context);

        public async Task OfferUserAJoke(IDialogContext context, string message)
        {
            PromptDialog.Confirm(
                    context: context,
                    resume: CheckUserWantsAJoke,
                    prompt: message,
                    retry: "Opção escolhida inválida. Favor, escolher uma das disponíveis.",
                    promptStyle: PromptStyle.Auto
                );
        }

        private async Task TellOneJoke(IDialogContext context)
        {
            var formDialog = new FormDialog<ChooseJokes>(new ChooseJokes(), ChooseJokes.BuildForm, FormOptions.PromptInStart);
            context.Call(formDialog, ExecuteAfterForm);
        }

        private async Task ExecuteAfterForm(IDialogContext context, IAwaitable<ChooseJokes> result)
        {
            var data = await result;
            var message = context.MakeMessage();

            message.Text = data.JokeType.GetDescribe();           
            await context.Forward(new JokeDialog(), ExecuteAfterJokeDialog, message, CancellationToken.None);
        }

        private async Task ExecuteAfterJokeDialog(IDialogContext context, IAwaitable<object> result)
        {
            var operation = (OperationCompletedHelper)(await result);

            if (operation.WasSuccessfull())
                await AsksUserForAJoke(context);
            else
                context.Wait(MessageReceived);
        }

        private async Task AsksUserForAJoke(IDialogContext context)
        {
            PromptDialog.Confirm(
                    context: context,
                    resume: CheckUserWantsAJoke,
                    prompt: "Quer ouvir outra...?      ° ͜ʖ﻿ ͡°",
                    retry: "Opção escolhida inválida. Favor, escolher uma das disponíveis.",
                    promptStyle: PromptStyle.Auto
                );
        }

        private async Task CheckUserWantsAJoke(IDialogContext context, IAwaitable<bool> result)
        {
            var wishAJoke = await result;

            if (!wishAJoke)
            {
                await SendEndConversationMessage(context);
                FinalizeContextWithSuccess(context);
            }
            else await TellOneJoke(context);
        }

        private async Task SendEndConversationMessage(IDialogContext context) =>
            await context.PostAsync("Beleza então... qualquer coisa, estarei aqui ^^");
    }
}