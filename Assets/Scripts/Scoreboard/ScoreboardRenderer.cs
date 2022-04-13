
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public void Render(List<Word> correct)
    {
        text.text = "";
        foreach (var word in correct)
        {
            text.text += (string)word;
            text.text += "\n";
        }
    }
}
