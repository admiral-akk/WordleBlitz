using System.Collections.Generic;
using System.Linq;

public static class Language
{
    private static readonly string AllCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static IEnumerable<char> Alphabet => AllCharacters.AsEnumerable();

    public static bool IsAlpha(char c)
    {
        return Alphabet.Any(a => a.ToString().ToUpper() == c.ToString().ToUpper());
    }
}