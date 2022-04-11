using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuessRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI guess;

    public void AddChar(char c)
    {
        text.text = text.text + c.ToString();
    }
}
