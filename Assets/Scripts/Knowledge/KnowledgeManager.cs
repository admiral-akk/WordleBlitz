using System.Collections;
using UnityEngine;
using static CharacterKnowledge;

public class KnowledgeManager : BaseManager
{
    [SerializeField] private int WordLength;

    public int Length => WordLength;

    private DictionaryManager _dictionary;
    
    private KeyboardKnowledge _keyboardKnowledge;
    public KeyboardKnowledge KeyboardKnowledge
    {
        get {
            if (_keyboardKnowledge == null)
                _keyboardKnowledge = new KeyboardKnowledge(WordLength);
            return _keyboardKnowledge;
        }
    }
    private GuessKnowledge _guessKnowledge;
    public GuessKnowledge GuessKnowledge
    {
        get
        {
            if (_guessKnowledge == null)
                _guessKnowledge = new GuessKnowledge(WordLength);
            return _guessKnowledge;
        }
    }

    private Word _answer;
    private Word Answer
    {
        get => _answer;
        set
        {
            _answer = value;
            GuessKnowledge.SetAnswer(_answer);
            KeyboardKnowledge.SetAnswer(_answer);
            Debug.LogFormat("New answer: {0}", _answer);
        }
    }


    public override IEnumerator Initialize()
    {
        Answer = "BLITZ";
        yield break;
    }

    public void RegisterDictionary(DictionaryManager dictionary)
    {
        _dictionary = dictionary;
    }

    public void UpdateKnowledge(Word guess)
    {
        KeyboardKnowledge.UpdateKnowledge(guess);
        GuessKnowledge.UpdateKnowledge(guess);
    }

    public AnnotatedWord Annotate(Word guess)
    {
        return GuessKnowledge.Annotate(guess);
    }

    public LetterKnowledge GlobalKnowledge(char c)
    {
        return KeyboardKnowledge[c];
    }

    public bool Correct(Word guess)
    {
        return guess == Answer;
    }

    public void NewProblem()
    {
        Answer = _dictionary.GetRandomWord(WordLength);
        GuessKnowledge.Clear();
        KeyboardKnowledge.Clear();
    }
}
