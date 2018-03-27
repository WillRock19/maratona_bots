using System;
using System.Collections.Generic;
using TelesBot.Interfaces;

namespace TelesBot.CustomResponses
{
    public class SimpleIntentionsResponses : ISimpleIntentionsResponses
    {
        public SimpleIntentionsResponses() { }

        public IReadOnlyCollection<string> Greetings() => new List<string>
        {
            "Oi nunca utilizado",
            "Hã... é... 'Oi' de novo... ☉_☉",
            "Ok... agora você só quer me provocar, né? ¬_¬",
            silenceGameQuestion()
        };

        public IReadOnlyCollection<string> GreetingsAfterTrolling() => new List<string>
        {
            "Cara, vai começar com isso de novo?! ¬_¬",
            "Na moral? Chega. (▀̿Ĺ̯▀̿ ̿)"
        };

        public IReadOnlyCollection<string> SmallTalk() => new List<string>
        {
            "Eu tô bem sim (☉‿☉). E você, como está?",
            "Eu... acho que já respondi isso... ☉_☉",
            "Seus pais sabem que você gosta de ser Troll? ¬_¬",
            silenceGameQuestion()
        };

        public IReadOnlyCollection<string> SmallTalkAfterTrolling() => new List<string>
        {
            "Hm... resolveu parar? Ok... tô bem sim (◡‿◡✿), e você?",
            "Ah, vai começar de novo, é? ¬_¬",
            "Toma outro **jogo do silêncio**! (▀̿Ĺ̯▀̿ ̿)"
        };

        private string silenceGameQuestion() => 
            "Diz aí: já brincou do **jogo do silêncio?** (▀̿Ĺ̯▀̿ ̿)";
    }
}