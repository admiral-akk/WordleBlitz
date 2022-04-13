using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Renderer.RenderGuess(guess);
    }

    public override IEnumerator Initialize()
    {
        Guesses.Clear();
        Renderer.Clear();
        yield break;
    }

    public List<Word> GetCorrectGuesses()
    {
        return _guesses
            .Where(g => g.Knowledge.All(k => k == CharacterKnowledge.LetterKnowledge.Here))
            .Select(g => g.Word)
            .ToList();
    }

    public override void ResetManager()
    {
        Guesses.Clear();
        Renderer.Clear();
    }
}
