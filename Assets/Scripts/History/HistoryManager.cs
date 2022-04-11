using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : BaseManager
{
    [SerializeField] private HistoryRenderer Renderer;

    private List<Word> _guesses;
    private List<Word> Guesses
    {
        get
        {
            if (_guesses == null)
                _guesses = new List<Word>();
            return _guesses;
        }
    }
    public void GuessSubmitted(Word guess)
    {
        Guesses.Add(guess);
        Renderer.RenderGuesses(Guesses);
    }
}
