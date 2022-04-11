public readonly struct GuessResult
{
    public enum State
    {
        None,
        IllegalWord,
        Wrong,
        Correct,
    }

    public readonly WordKnowledge Guess;
    public readonly State S;

    public GuessResult(Word guess, int maxLength, State s)
    {
        Guess = new WordKnowledge(guess, maxLength);
        S = s;
    }
}