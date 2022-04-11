public readonly struct GuessResult
{
    public enum State
    {
        None,
        IllegalWord,
        Wrong,
        Correct,
    }

    public readonly Word Guess;
    public readonly State S;

    public GuessResult(Word guess, State s)
    {
        Guess = guess;
        S = s;
    }
}