using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace TelesBot.CustomResponses
{
    [Serializable]
    public class Announcer
    {
        private string botImageUrl;
        private string botPorposeUrl;
        private string myNameIs;

        public Announcer()
        {
            myNameIs = "Olá, eu sou **Teles-Bot**! **(｡◕‿‿◕｡)**";
            botImageUrl = ConfigurationManager.AppSettings["BotImageUrl"];
            botPorposeUrl = ConfigurationManager.AppSettings["BotPorposeUrl"];
        }

        public HeroCard GenerateIntroduction() => new HeroCard()
        {
            Title = myNameIs,
            Text = "Sou um robô comediante! ¯| _(ツ)_ /¯. Eu te faço rir sobre:",
            Images = new List<CardImage>()
            {
                new CardImage(botImageUrl, "Sou eu, Teles-Bot! =D")
            },
            Buttons = JokesICanMakeButtons()
        };

        public HeroCard Help() => new HeroCard()
        {
            Title = "Skill Set",
            Text = WhatIDo(),
            Images = new List<CardImage>()
            {
                new CardImage(botPorposeUrl, "Então tu quem sou eu? (͡๏̯ ͡๏)")
            },
            Buttons = JokesICanMakeButtons()
        };

        private string WhatIDo() => @"Já se sentiu mal? Já se sentiu a raspa do taxo? Já se sentiu um lixo, o pior de todos, aquele cara que não merecia nem estar vivo? 
            Bom, não posso te ajudar nisso... talvez um bom psicólogo ¯|..(ツ)../¯. O que eu posso fazer, e o que eu faço muito bem, é te ajudar a sorrir nessa vida miserável (∪ ◡ ∪).
            Eu sou **Teles-Bot**, o mais maravilhoso chatbot de piadas já criado (segundo meu criador ◔.◔)! Meu objetivo? Botar um sorriso nesse rosto! ♥‿♥. É só escolher uma das piadas que eu sei:";

        private IList<CardAction> JokesICanMakeButtons() => new List<CardAction>()
        {
            new CardAction()
            {
                Type = ActionTypes.MessageBack,
                Title = "De tiozão",
                Value = "Conte uma piada",
            },
            new CardAction()
            {
                Type = ActionTypes.MessageBack,
                Title = "Humor negro",
                Value = "Conte uma piada",
            },
            new CardAction()
            {
                Type = ActionTypes.MessageBack,
                Title = "Super-heróis",
                Value = "Invente algo sobre um herói",
            },
        };
    }
}