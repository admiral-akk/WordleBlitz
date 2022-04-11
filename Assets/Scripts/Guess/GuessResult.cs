public readonly struct GuessResult
{
    public enum State
    {
        None,
        IllegalWord,
        Wrong,
        Correct,
    }

    public readonly WordKnowledge Knowledge;
    public readonly State S;

    public GuessResult(Word guess, int maxLength, State s)
    {
        Knowledge = new WordKnowledge(guess, maxLength);
        S = s;
    }
    public GuessResult(WordKnowledge knowledge, State s)
    {
        Knowledge = knowledge;
        S = s;
    }
}