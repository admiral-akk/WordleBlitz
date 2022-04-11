using System.Collections;
using UnityEngine;

public class GuessManager : BaseManager
{
    [SerializeField] private GuessRenderer Renderer;
    [SerializeField, Range(1,10)] private int WordLength;

    private DictionaryManager _dictionary;
    private GuessKnowledge _knowledge;

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
        _knowledge = new GuessKnowledge(WordLength);
        Guess = "";
        yield break;
    }

    private WordKnowledge CurrentGuess => _knowledge.GenerateKnowledge(Guess, WordLength);

    public GuessResult AddChar(char c)
    {
        Guess += c;
        if (Guess.Length < WordLength)
            return new GuessResult(CurrentGuess, GuessResult.State.None);
        _knowledge.UpdateKnowledge(Guess, CurrentAnswer);
        var currentGuess = CurrentGuess;
        Guess = "";
        if (!_dictionary.IsValidWord(currentGuess.Word))
            return new GuessResult(currentGuess, GuessResult.State.IllegalWord);
        if (currentGuess.Word != CurrentAnswer)
            return new GuessResult(currentGuess, GuessResult.State.Wrong);
        GetNewAnswer();
        return new GuessResult(currentGuess, GuessResult.State.Correct);
    }

    private void GetNewAnswer()
    {
        CurrentAnswer = _dictionary.GetRandomWord(WordLength);
        _knowledge.Clear();
    }

    public void RegisterDictionary(DictionaryManager dictionary)
    {
        _dictionary = dictionary;
        CurrentAnswer = "BLITZ";
    }

}
