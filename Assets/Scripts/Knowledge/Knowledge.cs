using System.Collections.Generic;

public class Knowledge 
{
    private Word _answer;
    private List<Word> _guesses;

    public Knowledge(int wordLength)
    {

    }

    public void UpdateKnowledge(Word guess)
    {
        _guesses.Add(guess);
    }
}
