using System.Linq;
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

    public bool Correct => Knowledge.All(k => k == LetterKnowledge.Here);
    public bool NoMatching => Knowledge.All(k => k != LetterKnowledge.Here && k != LetterKnowledge.CouldBeHere);
}
