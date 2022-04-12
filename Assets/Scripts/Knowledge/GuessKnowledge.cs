using static CharacterKnowledge;

public class GuessKnowledge : Knowledge
{
    public GuessKnowledge(int wordLength) : base(wordLength)
    {
    }

    public LetterKnowledge Get(Word word, int index)
    {
        if (index < word.Length)
            return characterKnowledge[index][word[index]];
        return LetterKnowledge.NoKnowledge;
    }
}
