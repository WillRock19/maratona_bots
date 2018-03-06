using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace TelesBot.Dialogs
{
    public class BotDialog : LuisDialog<object>
    {
        public BotDialog(ILuisService service) : base(service) { }

        [LuisIntent("None")]
        public void NoneAsync(IDialogContext context, LuisResult result)
        {

        }

        [LuisIntent("Cumprimento")]
        public void CumprimentoAsync(IDialogContext context, LuisResult result)
        {

        }

        [LuisIntent("trocar_ideia")]
        public void TrocarIdeiaAsync(IDialogContext context, LuisResult result)
        {

        }
    }
}