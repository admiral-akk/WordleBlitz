using System.Collections;
using UnityEngine;

public class GuessManager : BaseManager
{
    [SerializeField] private GuessRenderer Renderer;

    private DictionaryManager _dictionary;
    private KnowledgeManager _knowledge;

    private Word _guess;
    private Word Guess
    {
        get => _guess;
        set
        {
            _guess = value;
            Renderer.UpdateGuess(_knowledge.Annotate(_guess), _knowledge.Length);
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
    public GuessResult HandleInput(PlayerInput input)
    {
        switch (input.InputType)
        {
            case PlayerInput.Type.None:
                break;
            case PlayerInput.Type.Enter:
                if (Guess.Length < _knowledge.Length)
                    return new GuessResult(Guess, GuessResult.State.TooShort);
                if (!_dictionary.IsUsedWord("BLITZ") && Guess != "BLITZ")
                    return new GuessResult(Guess, GuessResult.State.NonBlitz);
                if (!_dictionary.IsValidWord(Guess))
                    return new GuessResult(Guess, GuessResult.State.InvalidWord);
                if (_dictionary.IsUsedWord(Guess))
                    return new GuessResult(Guess, GuessResult.State.ReusedWord);
                var guess = Guess;
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

    public void RegisterDictionary(DictionaryManager dictionary)
    {
        _dictionary = dictionary;
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
}
