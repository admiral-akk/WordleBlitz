using System.Collections.Generic;
using UnityEngine;

public class HistoryRenderer : MonoBehaviour
{ 
    [SerializeField] private GameObject WordPrefab;
    [SerializeField] private Canvas WordList;
    [SerializeField, Range(3,12)] private int WordLimit;

    private List<WordRenderer> _words;
    private List<WordRenderer> Words
    {
        get
        {
            if (_words == null)
                _words = new List<WordRenderer>();
            return _words;
        }
    }
    public void RenderGuess(AnnotatedWord guess)
    {
        if (Words.Count > WordLimit - 1)
        {
            Destroy(_words[0].gameObject);
            Words.RemoveAt(0);
        }
        var word = Instantiate(WordPrefab, WordList.transform).GetComponent<WordRenderer>();
        word.UpdateWord(guess, guess.Word.Length);
        Words.Add(word);
    }

    public void Clear()
    {
        foreach (var word in Words)
        {
            Destroy(word.gameObject);
        }
        Words.Clear();
    }
}
