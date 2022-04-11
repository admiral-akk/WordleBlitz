using TMPro;
using UnityEngine;

public class LetterRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void Set(char c)
    {
        text.text = c.ToString();
    }
    public void Clear()
    {
        text.text = "";
    }
}
