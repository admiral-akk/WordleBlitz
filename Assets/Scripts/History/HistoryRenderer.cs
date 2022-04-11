using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class HistoryRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void RenderGuesses(List<Word> guesses)
    {
        var sb = new StringBuilder();
        foreach (var guess in guesses)
        {
            sb.AppendLine((string)guess);
        }
        text.text = sb.ToString();
    }
}
