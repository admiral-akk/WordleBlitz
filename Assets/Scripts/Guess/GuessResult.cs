public readonly struct GuessResult
{
    public enum State
    {
        None,
        InvalidWord,
        TooShort,
        ReusedWord,
        Valid,
    }

    public readonly Word Guess;
    public readonly State S;

    public GuessResult(Word guess, State s)
    {
        Guess = guess;
        S = s;
    }

    public GuessResult(State s)
    {
        S = s;
        Guess = "";
    }
}