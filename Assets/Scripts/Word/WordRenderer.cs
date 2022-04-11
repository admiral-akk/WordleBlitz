using System.Collections.Generic;
using UnityEngine;

public class WordRenderer : MonoBehaviour
{
    [SerializeField] private GameObject LetterPrefab;

    private List<LetterRenderer> _letters;
    private List<LetterRenderer> Letters
    {
        get
        {
            if (_letters == null)
                _letters = new List<LetterRenderer>();
            return _letters;
        }
    }

    private void AddLetter()
    {
        _letters.Add(Instantiate(LetterPrefab, transform).GetComponent<LetterRenderer>());
    }

    public void UpdateWord(WordKnowledge knowledge)
    {
        while (Letters.Count < knowledge.MaxLength)
            AddLetter();
        var word = knowledge.Word;
        for (var i = 0; i < Letters.Count; i++)
        {
            if (i < word.Length)
            {
                _letters[i].Set(word[i], knowledge[i]);
            }
            else
            {
                _letters[i].Clear();
            }
        }
    } 
}
