using System.Linq;
using static CharacterKnowledge;

public readonly struct AnnotatedWord
{
    public readonly Word Word;
    public readonly LetterKnowledge[] Knowledge;
    public readonly int AnswerIndex;

    public AnnotatedWord(Word word, LetterKnowledge[] knowledge, int answerIndex = -1)
    {
        Word = word;
        Knowledge = knowledge;
        AnswerIndex = answerIndex;
    }

    public bool Correct => Knowledge.All(k => k == LetterKnowledge.Here);
    public bool NoMatching => Knowledge.All(k => k != LetterKnowledge.Here && k != LetterKnowledge.CouldBeHere);
}
