
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HistoryManager : MonoBehaviour, IUpdateObserver<GuessAnnotated> {
    [SerializeField] private HistoryRenderer Renderer;

    private List<AnnotatedWord> _guesses;
    private List<AnnotatedWord> Guesses => _guesses ??= new List<AnnotatedWord>();
    public void GuessSubmitted(AnnotatedWord guess) {
        Guesses.Add(guess);
        Renderer.RenderGuess(guess);
    }

    public void Awake() {
        Guesses.Clear();
        Renderer.Clear();
    }

    public List<Word> GetCorrectGuesses() {
        return _guesses
            .Where(g => g.Knowledge.All(k => k == CharacterKnowledge.LetterKnowledge.Here) && g.Word != "BLITZ")
            .Select(g => g.Word)
            .ToList();
    }

    public List<AnnotatedWord> GetHistory() {
        return new List<AnnotatedWord>(Guesses);
    }

    public void Handle(GuessAnnotated update) {
        Guesses.Add(update.AnnotatedGuess);
        Renderer.RenderGuess(update.AnnotatedGuess);
    }
}
