using System.ComponentModel;

namespace JokeApi.Helpers
{
    public enum JokeCategory
    {
        [Description("Nerd")]
        Nerd,

        [Description("Tiozão")]
        DadJokes,

        [Description("Especial")]
        Special,

        [Description("Heroi_Batman")]
        Hero_Batman,

        [Description("Heroi_Superman")]
        Hero_Superman,

        [Description("Heroi_Xmen")]
        Hero_Xmen,

        [Description("Heroi_Hulk")]
        Hero_Hulk,

        [Description("Heroi_Daredevil")]
        Hero_Daredevil,

        [Description("Piada sem categoria")]
        Undefined
    }
}
