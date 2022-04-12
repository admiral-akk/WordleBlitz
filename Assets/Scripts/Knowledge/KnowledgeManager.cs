using System.Collections;
using UnityEngine;

public class KnowledgeManager : BaseManager
{
    [SerializeField] private int WordLength;

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


    public override IEnumerator Initialize()
    {
        yield break;
    }

    public void RegisterDictionary(DictionaryManager dictionary)
    {
        _dictionary = dictionary;
        var answer = dictionary.GetRandomWord(WordLength);
        KeyboardKnowledge.SetAnswer(answer);
        GuessKnowledge.SetAnswer(answer);
    }

    public void UpdateKnowledge(Word guess)
    {
        KeyboardKnowledge.UpdateKnowledge(guess);
        GuessKnowledge.UpdateKnowledge(guess);
    }
}
