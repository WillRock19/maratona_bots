using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using TelesBot.Extensions;
using TelesBot.Enums;

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
            Text = "Sou um robô comediante xD. Eu vivo pra fazer rir! ¯| _(ツ)_ /¯",
            Images = new List<CardImage>()
            {
                new CardImage(botImageUrl, "Sou eu, Teles-Bot! =D")
            }
        };

        public HeroCard Help() => new HeroCard()
        {
            Title = "Skill Set",
            Text = WhatIDo(),
            Images = new List<CardImage>()
            {
                new CardImage(botPorposeUrl, "Então tu quer saber quem sou eu? (͡๏̯ ͡๏)")
            },
        };

        public HeroCard JokesICanMake() => new HeroCard()
        {
            Title = "Minhas piadas.",
            Buttons = JokesICanMakeInButtons()
        };

        private string WhatIDo() => @"Já se sentiu mal? Já se sentiu a raspa do taxo? Já se sentiu um lixo, o pior de todos, aquele cara que não merecia nem estar vivo? 
            Bom, não posso te ajudar nisso... talvez um bom psicólogo ¯|..(ツ)../¯. O que eu posso fazer, e o que eu faço muito bem, é te ajudar a sorrir nessa vida miserável (∪ ◡ ∪).
            Eu sou **Teles-Bot**, o mais maravilhoso chatbot de piadas já criado (segundo meu criador ◔.◔)! Meu objetivo? Botar um sorriso nesse rosto! ♥‿♥. É só dizer algo como 
            **'conta uma piada'** ou **'quero uma piada'** que podemos começar xD";

        private IList<CardAction> JokesICanMakeInButtons() => new List<CardAction>()
        {
            new CardAction()
            {
                Type = ActionTypes.PostBack,
                Title = JokeCategory.Nerd.GetDescribe(),
                Value = "Conte uma piada Nerd",
            },
            new CardAction()
            {
                Type = ActionTypes.PostBack,
                Title = JokeCategory.Special.GetDescribe(),
                Value = "Conte uma piada Especial"
            },
            new CardAction()
            {
                Type = ActionTypes.PostBack,
                Title = JokeCategory.DadJokes.GetDescribe(),
                Value = "Conte uma piada de Tiozao",
            },
            new CardAction()
            {
                Type = ActionTypes.PostBack,
                Title = JokeCategory.SuperHeroes.GetDescribe(),
                Value = "Conte uma piada de um Super-herói",
            },
        };
    }
}