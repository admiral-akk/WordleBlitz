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
    public Word? HandleInput(PlayerInput input)
    {
        Debug.Log(input);
        switch (input.InputType)
        {
            case PlayerInput.Type.None:
                break;
            case PlayerInput.Type.Enter:
                Debug.Log(Guess.Length);
                Debug.Log(_knowledge.Length);
                if (Guess.Length < _knowledge.Length)
                    break;
                Debug.Log(Guess);
                if (!_dictionary.IsValidWord(Guess))
                    break;
                var guess = Guess;
                Guess = "";
                return guess;
            case PlayerInput.Type.Delete:
                Guess = Guess.RemoveEnd();
                break;
            case PlayerInput.Type.HitKey:
                if (Guess.Length < _knowledge.Length)
                    Guess += input.Letter;
                break;
        }
        return null;
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

}
