using System.Collections;
using UnityEngine;

public class GuessManager : BaseManager
{
    [SerializeField] private GuessRenderer Renderer;
    [SerializeField, Range(1,10)] private int WordLength;

    private DictionaryManager _dictionary;
    private KnowledgeManager _knowledge;

    private Word _guess;
    private Word Guess
    {
        get => _guess;
        set
        {
            _guess = value;
            Renderer.UpdateGuess(new WordKnowledge(_guess, WordLength));
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

    public override IEnumerator Initialize()
    {
        Guess = "";
        yield break;
    }

    public Word? HandleInput(PlayerInput input)
    {
        switch (input.InputType)
        {
            case PlayerInput.Type.None:
                break;
            case PlayerInput.Type.Enter:
                if (Guess.Length < WordLength)
                    break;
                if (!_dictionary.IsValidWord(Guess))
                    break;
                var guess = Guess;
                Guess = "";
                return guess;
            case PlayerInput.Type.Delete:
                Guess = Guess.RemoveEnd();
                break;
            case PlayerInput.Type.HitKey:
                if (Guess.Length < WordLength)
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
    }

}
