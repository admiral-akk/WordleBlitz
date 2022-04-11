public class WordKnowledge
{
    public enum LetterKnowledge
    {
        None,
        Correct,
        WrongPosition,
        Wrong,
        Default,
    }

    private Word _guess;
    private LetterKnowledge[] _knowledge;

    public WordKnowledge(Word word, int maxLength)
    {
        _guess = word;
        _knowledge = new LetterKnowledge[maxLength];
    }

    public int MaxLength => _knowledge.Length;
    public Word Word => _guess;
    public LetterKnowledge this[int i]
    {
        get => _knowledge[i];
        set => _knowledge[i] = value;
    }
}