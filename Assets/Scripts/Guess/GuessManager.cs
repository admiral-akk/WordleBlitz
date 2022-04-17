using System.Collections.Generic;
using UnityEngine;


public class GuessEntered : BaseUpdate<GuessEntered> {
    public Word Guess;
    public GuessEntered(Word guess) {
        Guess = guess;
    }
}

public class GuessError : BaseUpdate<GuessError> {
    public enum Type {
        None,
        TooShort,
        InvalidWord,
        ReusedWord,
        NonBlitz,
    }
        public Type _type;

    public GuessError(Type type) {
        _type = type;
    }
}
public class GuessData : NewBaseData<GuessEntered> {

    private List<Word> _guesses;
    public List<Word> PreviousGuesses => _guesses ??= new List<Word>();
    public override void Handle(GuessEntered update) {
        PreviousGuesses.Add(update.Guess);
    }
}

public class GuessManager : NewBaseManager<GuessData, GuessEntered>,
    IUpdateObserver<ValidLexiconInitialized>, 
    IUpdateObserver<PlayerInput> {
    [SerializeField] private GuessRenderer Renderer;

    private IWordValidator _wordValidator;
    private KnowledgeManager _knowledge;

    private Word _guess;
    private Word Guess
    {
        get => _guess;
        set
        {
            _guess = value;
            if (_knowledge != null)
                Renderer.UpdateGuess(_knowledge.Annotate(_guess).Item2, _knowledge.Length);
        }
    }

    private HashSet<Word> _usedWords;
    private HashSet<Word> UsedWords => _usedWords ??= new HashSet<Word>();

    private GuessData _data;
    protected override GuessData Data {
        get => _data ??= new GuessData();
    }
    public GuessResult HandleInput(PlayerInput input)
    {
        switch (input.InputType)
        {
            case PlayerInput.Type.None:
                break;
            case PlayerInput.Type.Enter:
                if (!UsedWords.Contains("BLITZ") && Guess != "BLITZ")
                    return new GuessResult(Guess, GuessResult.State.NonBlitz);
                if (Guess.Length < _knowledge.Length)
                    return new GuessResult(Guess, GuessResult.State.TooShort);
                if (!_wordValidator.Valid(Guess))
                    return new GuessResult(Guess, GuessResult.State.InvalidWord);
                if (UsedWords.Contains(Guess))
                    return new GuessResult(Guess, GuessResult.State.ReusedWord);
                var guess = Guess;
                _usedWords.Add(guess);
                Guess = "";
                return new GuessResult(guess, GuessResult.State.Valid);
            case PlayerInput.Type.Delete:
                Guess = Guess.RemoveEnd();
                break;
            case PlayerInput.Type.HitKey:
                if (Guess.Length < _knowledge.Length)
                    Guess += input.Letter;
                break;
        }
        return new GuessResult(GuessResult.State.None);
    }

    public void RegisterValidator(IWordValidator wordValidator) {
        _wordValidator = wordValidator;
        Guess = "";
    }

    public void RegisterKnowledge(KnowledgeManager knowledge)
    {
        _knowledge = knowledge;
        Guess = "";
    }

    public override void ResetManager()
    {
        Guess = "";
    }

    public void Handle(ValidLexiconInitialized update) => _wordValidator = update.Validator;

    public void Handle(PlayerInput update) {
        return;
    }
}
