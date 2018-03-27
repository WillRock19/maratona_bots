using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TelesBot.Dependency;

namespace TelesBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Conversation.UpdateContainer(builder =>
            {
                builder.RegisterModule(new DialogModule());

                builder.RegisterModule(new BotModules());
            });

            //config.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);
        }
    }
}
