using System.Collections.Generic;
using UnityEngine;

public class GuessManager : BaseManager, IUpdateObserver<ValidLexiconInitialized>
{
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


    private Word _currentAnswer;
    private Word CurrentAnswer
    {
        get => _currentAnswer;
        set
        {
            _currentAnswer = value;
        }
    }

    private HashSet<Word> _usedWords;
    private HashSet<Word> UsedWords => _usedWords ??= new HashSet<Word>();
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
}
