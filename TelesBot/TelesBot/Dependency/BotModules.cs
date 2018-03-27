using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using System.Net.Http;
using TelesBot.CustomResponses;
using TelesBot.Dialogs;
using TelesBot.Helpers;
using TelesBot.Interfaces;
using TelesBot.Services;

namespace TelesBot.Dependency
{
    public class BotModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //Register RootDialog as IDialog<object>
            builder.RegisterType<BotDialog>()
                   .As<IDialog<object>>()
                   .InstancePerDependency();

            ////We will come to this later
            builder.RegisterType<DialogFactory>()
              .Keyed<IDialogFactory>(FiberModule.Key_DoNotSerialize)
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            //Register Dialogs 
            //builder.RegisterType<BotDialog>().InstancePerDependency();
            builder.RegisterType<JokeDialog>().InstancePerDependency();

            //Register bot specific services, make sure you Key them as DoNotSerialize
            builder.RegisterType<Announcer>()
              .Keyed<IAnnouncer>(FiberModule.Key_DoNotSerialize)
              .AsImplementedInterfaces()
              .InstancePerDependency();

            builder.RegisterType<SimpleResponsesGenerator>()
              .Keyed<ISimpleResponsesGenerator>(FiberModule.Key_DoNotSerialize)
              .AsImplementedInterfaces()
              .InstancePerDependency();

            builder.RegisterType<SimpleIntentionsResponses>()
              .Keyed<ISimpleIntentionsResponses>(FiberModule.Key_DoNotSerialize)
              .AsImplementedInterfaces()
              .InstancePerDependency();

            builder.RegisterType<QnaMakerHelper>()
              .Keyed<IQnaMakerHelper>(FiberModule.Key_DoNotSerialize)
              .AsImplementedInterfaces()
              .InstancePerDependency();

            builder.RegisterType<JokeSearcher>()
              .Keyed<IJokeSearcher>(FiberModule.Key_DoNotSerialize)
              .AsImplementedInterfaces()
              .InstancePerDependency();

            builder.RegisterType<ImageRecognitionHandler>()
              .Keyed<IImageRecognitionHandler>(FiberModule.Key_DoNotSerialize)
              .AsImplementedInterfaces()
              .InstancePerDependency();

            builder.RegisterType<CardGeneratorHelper>()
              .Keyed<ICardGeneratorHelper>(FiberModule.Key_DoNotSerialize)
              .AsImplementedInterfaces()
              .InstancePerDependency();

            //Register 
            builder.RegisterType<HttpClient>()
              .Keyed<HttpClient>(FiberModule.Key_DoNotSerialize)
              .AsSelf()
              .InstancePerDependency();
        }
    }
}