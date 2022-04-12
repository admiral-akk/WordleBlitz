using System.Collections.Generic;
using UnityEngine;

public class WordDictionary
{
    private HashSet<Word> _words;
    public WordDictionary()
    {
        _words = new HashSet<Word>();
    }

    public int Count => _words.Count;

    public void AddWord(Word word)
    {
        _words.Add(word);
    } 

    public bool IsValidWord(Word word)
    {
        return _words.Contains(word);
    }

    public Word GetRandomWord()
    {
        var index = Random.Range(0, _words.Count);
        var enumerator = _words.GetEnumerator();
        while (index-- > 0)
            enumerator.MoveNext();
        return enumerator.Current;
    }
}