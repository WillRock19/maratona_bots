using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Connector;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using TelesBot.CustomResponses;

namespace TelesBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private ConnectorClient connector;

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            await HandleSystemActivity(activity);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private async Task HandleSystemActivity(Activity activity)
        {
            switch (activity.Type)
            {
                case ActivityTypes.ConversationUpdate:
                    IntroductBotForNewUsers(activity);
                    break;

                case ActivityTypes.Message:

                    try
                    {
                        using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                        {
                            var dialog = scope.Resolve<IDialog<object>>();
                            await Conversation.SendAsync(activity, () => dialog);
                        }

                        //await Conversation.SendAsync(activity, () => new Dialogs.BotDialog());
                    }
                    catch (Exception e)
                    {
                        var a =  e.Message;
                    }


                    break;

                case ActivityTypes.ContactRelationUpdate:
                    // Handle add/remove from contact lists
                    // Activity.From + Activity.Action represent what happened
                    break;

                case ActivityTypes.Typing:
                    // Handle knowing tha the user is typing
                    break;

                case ActivityTypes.Ping:

                    break;
            }
        }

        private void IntroductBotForNewUsers(Activity activity)
        {
            if (activity.MembersAdded != null && activity.MembersAdded.Any())
            {
                var botId = activity.Recipient.Id;
                var test = activity.MembersAdded.Select(m => m).Where(m => m.Id != botId).ToList();

                if (activity.MembersAdded.Any(m => m.Id != botId))
                {
                    var announcer = new Announcer();
                    var reply = activity.CreateReply();

                    reply.Attachments.Add(announcer.GenerateIntroduction().ToAttachment());
                    connector.Conversations.SendToConversationAsync(reply);
                }
            }
        }
    }
}