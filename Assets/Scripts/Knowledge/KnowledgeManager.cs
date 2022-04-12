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

    private string Answer
    {
        set
        {
            GuessKnowledge.SetAnswer(value);
            KeyboardKnowledge.SetAnswer(value);
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
}