using System.Collections.Generic;
using System.Linq;
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

    private void CopyGuessesToClipboard()
    {
        GUIUtility.systemCopyBuffer = _renderedGuesses;
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

    private void OnGUI()
    {

        var e = Event.current;
        if (e == null)
            return;
        if (e.type != EventType.KeyDown)
            return;
        if (e.keyCode == KeyCode.LeftControl)
            CopyGuessesToClipboard();
    }
}
