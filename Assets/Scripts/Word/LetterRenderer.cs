using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void Set(char c, WordKnowledge.LetterKnowledge k)
    {
        text.text = c.ToString();
        switch (k)
        {
            case WordKnowledge.LetterKnowledge.None:
                S = State.None;
                break;
            case WordKnowledge.LetterKnowledge.Correct:
                S = State.Correct;
                break;
            case WordKnowledge.LetterKnowledge.WrongPosition:
                S = State.None;
                break;
            case WordKnowledge.LetterKnowledge.Wrong:
                S = State.Wrong;
                break;
            case WordKnowledge.LetterKnowledge.Default:
                S = State.Default;
                break;
        }
    }
    public void Clear()
    {
        text.text = "";
        S = State.Default;
    }
}
