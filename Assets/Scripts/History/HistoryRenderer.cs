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
    public void RenderGuesses(List<AnnotatedWord> guesses)
    {
        foreach (var word in Words)
        {
            Destroy(word.gameObject);
        }
        Words.Clear();
        if (guesses.Count > WordLimit)
            guesses = guesses.GetRange(guesses.Count - WordLimit, WordLimit);
        foreach (var guess in guesses)
        {
            var word = Instantiate(WordPrefab, WordList.transform).GetComponent<WordRenderer>();
            word.UpdateWord(guess, guess.Word.Length);
            _words.Add(word);
        }
    }
}
