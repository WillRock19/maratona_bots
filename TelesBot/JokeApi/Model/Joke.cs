using JokeApi.Extensions;
using JokeApi.Helpers;

namespace JokeApi.Model
{
    public class Joke
    {
        public string Introduction { get; set; }
        public string Conclusion { get; set; }
        public JokeCategory Category { get; set; }
        public string CategoryDescription { get; set; }

        public Joke() { }

        public Joke(string introduction,  string conclusion = "", string category = "")
        {
            Introduction = introduction;
            Conclusion = conclusion;
            CategoryDescription = category;
            Category = !string.IsNullOrEmpty(category) ? category.ToEnumThatHasThisDescription<JokeCategory>() : JokeCategory.Undefined;
        }
    }
}
