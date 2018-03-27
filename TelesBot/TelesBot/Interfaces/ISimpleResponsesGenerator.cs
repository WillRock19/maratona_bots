using System.Threading.Tasks;

namespace TelesBot.Interfaces
{
    public interface ISimpleResponsesGenerator
    {
        Task<string> RespondGreeting(string greeting);
        string RespondSmallTalk();
        string RespondUserPositiveStateOfMind();
        string RespondUserNegativeStateOfMind();
    }
}