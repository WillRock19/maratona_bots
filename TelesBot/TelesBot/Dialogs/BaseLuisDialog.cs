using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using TelesBot.Helpers;

namespace TelesBot.Dialogs
{
    [Serializable]
    public class BaseLuisDialog : LuisDialog<object>
    {
        public BaseLuisDialog() : base(CreateNewService()) { }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task NenhumaIntencao(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Eiiita... eu não sei responder isso **(╥_╥)**");
            FinalizeContextWithFail(context);
        }

        protected void FinalizeContextWithFail(IDialogContext context, string mensagem = "")
        {
            if(!string.IsNullOrWhiteSpace(mensagem))
                context.Fail(new Exception(mensagem));
            else
                context.Fail(new NotImplementedException("Desculpe, não fui programado para esse responder tipo de coisa **(╥_╥)**"));
        }

        protected void FinalizeContextWithDone(IDialogContext context) => context.Done(String.Empty);

        protected async Task SendIsTypingMessage(IDialogContext context)
        {
            var reply = context.MakeMessage();
            reply.Type = ActivityTypes.Typing;

            await context.PostAsync(reply);
        }

        protected async Task SendIsTyping<T>(IDialogContext context, Task<T> resultForWaitTo)
        {
            while (!resultForWaitTo.IsCompleted)
            {
                await SendIsTypingMessage(context);
                await Task.Delay(3000);
            }
        }

        private static ILuisService CreateNewService()
        {
            var luisModel = new LuisModelAttribute(ConfigurationManager.AppSettings["LuisModelId"], ConfigurationManager.AppSettings["LuisSubscriptionKey"]);
            return new LuisService(luisModel);
        }
    }
}