using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : BaseManager
{
    [SerializeField] private HistoryRenderer Renderer;

    private List<AnnotatedWord> _guesses;
    private List<AnnotatedWord> Guesses
    {
        get
        {
            if (_guesses == null)
                _guesses = new List<AnnotatedWord>();
            return _guesses;
        }
    }
    public void GuessSubmitted(AnnotatedWord guess)
    {
        Guesses.Add(guess);
        Renderer.RenderGuesses(Guesses);
    }
}
