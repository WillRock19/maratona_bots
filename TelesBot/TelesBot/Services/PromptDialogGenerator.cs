using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using TelesBot.Extensions;
using TelesBot.Interfaces;

namespace TelesBot.Services
{
    public class PromptDialogGenerator : IPromptDialogGenerator
    {
        private string defaultRetryMessage;
        private string defaultConfirmPromptMessage;


        public PromptDialogGenerator()
        {
            defaultRetryMessage = "Opção escolhida inválida. Favor, escolher uma das disponíveis.";
            //defaultConfirmPromptMessage;
        }

        public void ConfirmDialog(IDialogContext context, ResumeAfter<bool> executeAfterUserResponse, string promptMessage, string retryMessage = "", int attemptsAllowed = 3, PromptStyle style = PromptStyle.Auto)
        {
            PromptDialog.Confirm(
                context: context,
                resume: executeAfterUserResponse,
                prompt: promptMessage,
                retry: retryMessage.HasValue() ? retryMessage : defaultRetryMessage,
                attempts: attemptsAllowed,
                promptStyle: style
            );
        }
    }
}