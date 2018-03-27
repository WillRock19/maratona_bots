using System.Threading.Tasks;

namespace TelesBot.Interfaces
{
    public interface IQnaMakerHelper
    {
        Task<string> SearchForHighScoreAnswer(string query);
    }
}