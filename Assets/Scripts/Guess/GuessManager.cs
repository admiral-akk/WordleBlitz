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
            Renderer.UpdateGuess(_guess, WordLength);
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
    public GuessResult AddChar(char c)
    {
        Guess += c;
        if (Guess.Length < WordLength)
            return new GuessResult("", GuessResult.State.None);
        var submittedGuess = Guess;
        Guess = "";
        if (!_dictionary.IsValidWord(submittedGuess))
            return new GuessResult("", GuessResult.State.IllegalWord);
        if (submittedGuess != CurrentAnswer)
            return new GuessResult(submittedGuess, GuessResult.State.Wrong);
        GetNewAnswer();
        return new GuessResult(submittedGuess, GuessResult.State.Correct);
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
