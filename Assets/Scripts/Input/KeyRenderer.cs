using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyRenderer : MonoBehaviour
{
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
}
