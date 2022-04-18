using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterKnowledge;

public class KeyRenderer : BaseLetterRenderer
{
    [SerializeField] private Button button;

    private char _key;
    public char Key
    {
        get => _key;
        set
        {
            _key = value.ToString().ToUpper()[0];
            Render(Key.ToString(), Knowledge);
        }
            
    }

    private LetterKnowledge _knowledge;
    public LetterKnowledge Knowledge
    {
        private get => _knowledge;
        set
        {
            _knowledge = value;
            Render(Key.ToString(), Knowledge);
        }
    }


    public Action Callback
    {
        set
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => value());
        }
    }
}
