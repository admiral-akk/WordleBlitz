using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterKnowledge;

public class LetterRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image background;

    public enum State { 
        None,
        Wrong,
        WrongPosition,
        Correct,
        Default,
    }

    private State _s;
    private State S
    {
        get => _s;
        set
        {
            _s = value;
            switch (_s)
            {
                case State.None:
                    background.color = ColorPaletteManager.ColorPalette.None;
                    break;
                case State.Wrong:
                    background.color = ColorPaletteManager.ColorPalette.Wrong;
                    break;
                case State.Correct:
                    background.color = ColorPaletteManager.ColorPalette.Correct;
                    break;
                case State.WrongPosition:
                    background.color = ColorPaletteManager.ColorPalette.WrongPosition;
                    break;
                case State.Default:
                    background.color = ColorPaletteManager.ColorPalette.Default;
                    break;
            }
        }
    }

    public void Set(char c, LetterKnowledge k)
    {
        if (text.text == c.ToString())
            return;
        text.text = c.ToString();
        switch (k)
        {
            case LetterKnowledge.NoKnowledge:
                S = State.None;
                break;
            case LetterKnowledge.Here:
                S = State.Correct;
                break;
            case LetterKnowledge.NotHere:
            case LetterKnowledge.CouldBeHere:
                S = State.WrongPosition;
                break;
            case LetterKnowledge.NotInWord:
                S = State.Default;
                return;
        }
        gameObject.AddComponent(typeof(Pop));
    }
    public void Clear()
    {
        text.text = "";
        S = State.Default;
    }
}
