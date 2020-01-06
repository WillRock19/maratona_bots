using Microsoft.Bot.Builder.Dialogs;

namespace TelesBot.Interfaces
{
    public interface IPromptDialogGenerator
    {
        void ConfirmDialog(IDialogContext context, ResumeAfter<bool> executeAfterUserResponse, string promptMessage, string retryMessage = "", int attemptsAllowed = 3, PromptStyle style = PromptStyle.Auto);
    }
}
