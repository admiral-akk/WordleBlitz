using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class HistoryStringRenderer : MonoBehaviour
{
    [SerializeField] private Button Share;
    [SerializeField] private string Green;
    [SerializeField] private string Yellow;
    [SerializeField] private string Gray;

    private string _renderedGuesses;

    public void Initialize()
    {
        Share.onClick.AddListener(CopyGuessesToClipboard);
    }

    [DllImport("__Internal")]
    private static extern void CopyToClipboard(string text);

    public static void SetText(string text)
    {
#if UNITY_WEBGL && UNITY_EDITOR == false
            CopyToClipboard(text);
#else
        GUIUtility.systemCopyBuffer = text;
#endif
    }
    private void CopyGuessesToClipboard()
    {
        SetText(_renderedGuesses);
    }
    public void RenderGuesses(List<AnnotatedWord> guesses)
    {
        _renderedGuesses = "Wordle Blitz - 10s\n";
        foreach (var word in guesses)
        {
            if (word.Word == "BLITZ")
                continue;
            if (word.Correct)
            {
                _renderedGuesses += Green + "\n";
            } else if (word.NoMatching)
            {
                _renderedGuesses += Gray;
            } else
            {
                _renderedGuesses += Yellow;
            }
        }
    }

    public void Clear()
    {
        _renderedGuesses = "";
    }
}
