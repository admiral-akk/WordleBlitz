using static CharacterKnowledge;

public readonly struct AnnotatedWord
{
    public readonly Word Word;
    public readonly LetterKnowledge[] Knowledge;

    public AnnotatedWord(Word word, LetterKnowledge[] knowledge)
    {
        Word = word;
        Knowledge = knowledge;
    }
}
