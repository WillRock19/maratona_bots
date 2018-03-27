using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelesBot.Interfaces
{
    public interface IAnnouncer
    {
        HeroCard GenerateIntroduction();
        HeroCard Help();
    }
}
