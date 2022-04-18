using System.Collections.Generic;
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

public class GuessData : BaseRenderData<GuessData> {

    private AnnotatedWord _guess;
    public AnnotatedWord Guess {
        get => _guess;
        set {
            _guess = value;
            HasUpdate = true;
        }
    }

    public int MaxLength { get; set; }

    public GuessData() : base() {
        Guess = new AnnotatedWord("", new CharacterKnowledge.LetterKnowledge[0]);
    }
}

public class GuessManager : RendererManager<GuessData>,
    IUpdateObserver<GuessEntered>,
    IUpdateObserver<ValidLexiconInitialized>,
    IUpdateObserver<PlayerInput>,
    IUpdateObserver<KnowledgeInitialized> {
    private IWordValidator _wordValidator;
    private KnowledgeManager _knowledge;
    private Word Guess {
        get => Data.Guess.Word;
        set {
            if (_knowledge != null) {
                Data.MaxLength = _knowledge.Length;
                Data.Guess = _knowledge.Annotate(value).Item2;
            }
        }
    }

    private HashSet<Word> _usedWords;
    private HashSet<Word> UsedWords => _usedWords ??= new HashSet<Word>();

    private GuessData _data;
    protected override GuessData Data {
        get {
            if (_data == null)
                _data = new GuessData();
            return _data;
        }
    }

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
        UsedWords.Add(update.Guess);
    }
    #endregion
}
