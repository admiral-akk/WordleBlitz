using System.Collections.Generic;
using UnityEngine;

public class HistoryRenderer : MonoBehaviour
{ 
    [SerializeField] private GameObject WordPrefab;
    [SerializeField] private Canvas WordList;

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
    public void RenderGuesses(List<Word> guesses)
    {
        foreach (var word in Words)
        {
            Destroy(word.gameObject);
        }
        Words.Clear();
        foreach (var guess in guesses)
        {
            var word = Instantiate(WordPrefab, WordList.transform).GetComponent<WordRenderer>();
            word.UpdateWord(guess, guess.Length);
            _words.Add(word);
        }
    }
}
