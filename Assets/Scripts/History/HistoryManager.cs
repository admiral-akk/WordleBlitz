using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : BaseManager
{
    [SerializeField] private HistoryRenderer Renderer;

    private List<WordKnowledge> _guesses;
    private List<WordKnowledge> Guesses
    {
        get
        {
            if (_guesses == null)
                _guesses = new List<WordKnowledge>();
            return _guesses;
        }
    }
    public void GuessSubmitted(WordKnowledge guess)
    {
        Guesses.Add(guess);
        Renderer.RenderGuesses(Guesses);
    }
}
