using System.Collections.Generic;
using UnityEngine;

public class HistoryRenderer : BaseRenderer<HistoryData>
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

    protected override void Render(HistoryData data) {
        if (data.Guesses.Count == 0)
            return;
        if (Words.Count > 0 &&
            Words[Words.Count - 1].Current == data.Guesses[data.Guesses.Count - 1].Word)
            return;
        var word = Instantiate(WordPrefab, WordList.transform).GetComponent<WordRenderer>();
        var guess = data.Guesses[data.Guesses.Count - 1];
        word.UpdateWord(guess, guess.Word.Length);
        Words.Add(word);
        if (Words.Count > WordLimit) {
            Destroy(Words[0].gameObject);
            Words.RemoveAt(0);
        }
    }
}
