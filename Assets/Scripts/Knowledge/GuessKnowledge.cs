using System.Collections.Generic;
using static CharacterKnowledge;

public class GuessKnowledge : Knowledge
{
    public GuessKnowledge(int wordLength) : base(wordLength)
    {
    }

    public AnnotatedWord Annotate(Word word)
    {
        var knowledge = new LetterKnowledge[word.Length];
        for (var i = 0; i < word.Length; i++)
        {
            knowledge[i] = characterKnowledge[i][word[i]];
        }
        var dict = new Dictionary<char, int>();
        for (var i = 0; i < word.Length; i++)
        {
            var c = _answer[i];
            if (!dict.ContainsKey(c))
                dict[c] = 0;
            if (knowledge[i] != LetterKnowledge.Here)
                dict[c]++;
        }
        for (var i = 0; i < word.Length; i++)
        {
            var c = word[i];
            if (dict.ContainsKey(c) && knowledge[i] != LetterKnowledge.Here && dict[c] == 0)
                knowledge[i] = LetterKnowledge.NoMoreInWord;
        }
        return new AnnotatedWord(word, knowledge);
    }
}
