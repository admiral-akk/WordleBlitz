using System.Collections.Generic;
using UnityEngine;
using static CharacterKnowledge;

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

    private Word _current;

    private void Awake()
    {
        _current = "";
    }

    public void UpdateWord(AnnotatedWord knowledge, int maxLength)
    {
        while (Letters.Count < maxLength)
            AddLetter();
        var word = knowledge.Word;
        for (var i = 0; i < Letters.Count; i++)
        {
            if (i < word.Length)
            {
                Letters[i].Set(word[i], knowledge.Knowledge[i]);
                if (i >= _current.Length)
                    Letters[i].Pop();
            }
            else
            {
                Letters[i].Clear();
            }
        }
        _current = knowledge.Word;
    } 
}
