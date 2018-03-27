using System;
using System.Collections.Generic;

namespace TelesBot.Interfaces
{
    public interface ISimpleIntentionsResponses
    {
        IReadOnlyCollection<string> Greetings();
        IReadOnlyCollection<string> GreetingsAfterTrolling();
        IReadOnlyCollection<string> SmallTalk();
        IReadOnlyCollection<string> SmallTalkAfterTrolling();

    }
}
