using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterKnowledge;

public abstract class BaseRenderer : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;

    protected void Render(string s, LetterKnowledge k)
    {
        text.text = s;
        text.color = ColorPaletteManager.GetTextColor();
        background.color = ColorPaletteManager.GetColor(k);
    }
}
