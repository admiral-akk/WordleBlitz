
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public void Render(List<Word> correct)
    {
        text.text = "";
        for (var i = 0; i < correct.Count; i++)
        {
            text.text += string.Format("{0}. {1}\n", i+1,(string)correct[i]);
        }
    }
}
