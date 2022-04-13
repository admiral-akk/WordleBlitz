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

        var countHint = new Dictionary<char, int>();
        for (var i =0; i < word.Length; i++)
        {
            var c = word[i];
            if (knowledge[i] != LetterKnowledge.CouldBeHere && knowledge[i] != LetterKnowledge.Here)
                continue;
            if (!countHint.ContainsKey(c))
                countHint[c] = 0;
            countHint[c]++;
        }

        for (var i = 0; i < word.Length; i++)
        {
            var c = word[i];
            if (!countHint.ContainsKey(c) || !characterCount.ContainsKey(c) || countHint[c] <= characterCount[c])
                continue;
            for (var j = word.Length - 1; j >= 0; j--)
            {
                if (countHint[c] <= characterCount[c])
                    break;
                if (knowledge[j] != LetterKnowledge.CouldBeHere)
                    continue;
                knowledge[j] = LetterKnowledge.NoMoreInWord;
                countHint[c]--;
            }
        }
        return new AnnotatedWord(word, knowledge);
    }
}
