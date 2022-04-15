using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
        Renderer.RenderGuess(guess);
    }

    public override IEnumerator Initialize()
    {
        ResetManager();
        yield break;
    }

    public List<Word> GetCorrectGuesses()
    {
        return _guesses
            .Where(g => g.Knowledge.All(k => k == CharacterKnowledge.LetterKnowledge.Here) && g.Word != "BLITZ")
            .Select(g => g.Word)
            .ToList();
    }

    public List<AnnotatedWord> GetHistory()
    {
        return new List<AnnotatedWord>(Guesses);
    }

    public override void ResetManager()
    {
        Guesses.Clear();
        Renderer.Clear();
    }
}
