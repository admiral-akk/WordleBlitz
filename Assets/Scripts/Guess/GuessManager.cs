using System.Collections.Generic;
using UnityEngine;


public class GuessEntered : BaseUpdate<GuessEntered> {
    public Word Guess;
    public GuessEntered(Word guess) {
        Guess = guess;
    }
}

public class GuessError : BaseUpdate<GuessError> {
    public enum ErrorType {
        None,
        TooShort,
        InvalidWord,
        ReusedWord,
        NonBlitz,
    }
    public ErrorType Type;

    public GuessError(ErrorType type) {
        Type = type;
    }
}

public class GuessManager : MonoBehaviour,
    IUpdateObserver<GuessEntered>,
    IUpdateObserver<ValidLexiconInitialized>,
    IUpdateObserver<PlayerInput>,
    IUpdateObserver<KnowledgeInitialized> {
    [SerializeField] private GuessRenderer Renderer;

    private IWordValidator _wordValidator;
    private KnowledgeManager _knowledge;
    private List<Word> _guesses;
    public List<Word> Guesses => _guesses ??= new List<Word>();

    private Word _guess;
    private Word Guess {
        get => _guess;
        set {
            _guess = value;
            if (_knowledge != null)
                Renderer.UpdateGuess(_knowledge.Annotate(_guess).Item2, _knowledge.Length);
        }
    }

    private HashSet<Word> _usedWords;
    private HashSet<Word> UsedWords => _usedWords ??= new HashSet<Word>();

    public void Handle(PlayerInput update) {
        switch (update.InputType) {
            case PlayerInput.Type.None:
                break;
            case PlayerInput.Type.Enter:
                if (!UsedWords.Contains("BLITZ") && Guess != "BLITZ") {
                    new GuessError(GuessError.ErrorType.NonBlitz).Emit(gameObject);
                    return;
                }
                if (Guess.Length < _knowledge.Length) {
                    new GuessError(GuessError.ErrorType.TooShort).Emit(gameObject);
                    return;
                }
                if (!_wordValidator.Valid(Guess)) {
                    new GuessError(GuessError.ErrorType.InvalidWord).Emit(gameObject);
                    return;
                }
                if (UsedWords.Contains(Guess)) {
                    new GuessError(GuessError.ErrorType.ReusedWord).Emit(gameObject);
                    return;
                }
                var guess = Guess;
                _usedWords.Add(guess);
                Guess = "";
                new GuessEntered(guess).Emit(gameObject);
                return;
            case PlayerInput.Type.Delete:
                Guess = Guess.RemoveEnd();
                break;
            case PlayerInput.Type.HitKey:
                if (Guess.Length < _knowledge.Length)
                    Guess += update.Letter;
                break;
        }
    }

    #region Observers
    public void Handle(KnowledgeInitialized update) {
        _knowledge = update.Knowledge;
        Guess = "";
    }

    public void Handle(ValidLexiconInitialized update) {
        _wordValidator = update.Validator;
        Guess = "";
    }

    public void Handle(GuessEntered update) {
        Guesses.Add(update.Guess);
    }
    #endregion
}
