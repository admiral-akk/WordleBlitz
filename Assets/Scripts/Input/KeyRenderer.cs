using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterKnowledge;

public class KeyRenderer : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;

    public char Key
    {
        get => text.text[0];
        set => text.text = value.ToString().ToUpper();
    }

    public Action Callback
    {
        set
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => value());
        }
    }

    public void UpdateKnowledge(LetterKnowledge k)
    {
        switch (k)
        {
            case LetterKnowledge.NoKnowledge:
                background.color = ColorPaletteManager.ColorPalette.Default;
                break;
            case LetterKnowledge.NotInWord:
                background.color = ColorPaletteManager.ColorPalette.None;
                break;
            case LetterKnowledge.NotHere:
            case LetterKnowledge.CouldBeHere:
                background.color = ColorPaletteManager.ColorPalette.WrongPosition;
                break;
            case LetterKnowledge.Here:
                background.color = ColorPaletteManager.ColorPalette.Correct;
                break;
            default:
                break;
        }
    }
}
