using System.Collections;
using UnityEngine;

public class GuessManager : BaseManager
{
    [SerializeField] private GuessRenderer Renderer;
    [SerializeField, Range(1,10)] private int WordLength;

    private DictionaryManager _dictionary;

    private Word _guess;
    private Word Guess
    {
        get => _guess;
        set
        {
            _guess = value;
            Renderer.UpdateGuess(_guess);
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

    private void CheckAnswer()
    {
        Debug.LogFormat("Current answer: {0}\nGuess: {1}", CurrentAnswer, Guess);
        if (!_dictionary.IsValidWord(Guess))
        {
            Debug.Log("Guess isn't valid word!");
        } else if (Guess == CurrentAnswer)
        {
            Debug.Log("Guess correct!");
            GetNewAnswer();
        } else
        {
            Debug.Log("Guess wrong!");
        }
        Guess = "";
    }
    public void AddChar(char c)
    {
        Guess += c;
        if (Guess.Length < WordLength)
            return;
        CheckAnswer();
    }

    private void GetNewAnswer()
    {
        CurrentAnswer = _dictionary.GetRandomWord(WordLength); 
    }

    public void RegisterDictionary(DictionaryManager dictionary)
    {
        _dictionary = dictionary;
        GetNewAnswer();
    }

}
