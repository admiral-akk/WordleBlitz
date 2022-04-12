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
        return new AnnotatedWord(word, knowledge);
    }
}
