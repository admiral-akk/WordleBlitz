using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShareStringRenderer : MonoBehaviour {
    [SerializeField] private Button Share;
    [SerializeField] private string Green;
    [SerializeField] private string Yellow;
    [SerializeField] private string Gray;
    [SerializeField] private Prompt CopyPrompt;
    [SerializeField] private string GameUrl;


    private string _renderedGuesses;

    public void Awake() {
        Share.onClick.AddListener(CopyGuessesToClipboard);
    }

    [DllImport("__Internal")]
    private static extern void CopyToClipboard(string text);

    public static void SetText(string text) {
        #if UNITY_WEBGL && UNITY_EDITOR == false
            CopyToClipboard(text);
        #else
            GUIUtility.systemCopyBuffer = text;
        #endif
    }
    private void CopyGuessesToClipboard() {
        SetText(_renderedGuesses);
        CopyPrompt.PromptAnimation(0.2f, 0.3f, 1.5f);
    }
    public void RenderGuesses(TimeSpan time, List<AnnotatedWord> guesses, List<Tuple<Word, int>> guessesRequired) {
        var sb = new StringBuilder();
        sb.AppendLine(string.Format("Wordle Blitz - {0}:{1:00.}", time.Minutes, time.Seconds));

        var strings = new List<string>();
        var wordToIndex = new Dictionary<Word, int>();
        var guessed = new List<bool>();
        for (var i = 0; i < guessesRequired.Count; i++) {
            strings.Add("");
            wordToIndex[guessesRequired[i].Item1] = i;
            guessed.Add(false);
        }

        var currentIndex = 0;

        foreach (var word in guesses) {
            while (guessed[currentIndex])
                currentIndex++;
            if (word.Word == "BLITZ")
                continue;

            if (word.NoMatching) {
                strings[currentIndex] += Gray;
            } else if (!word.Correct) {
                strings[currentIndex] += Yellow;
            }
            if (wordToIndex.ContainsKey(word.Word)) {
                guessed[wordToIndex[word.Word]] = true;
            }
        }
        foreach (var s in strings) {
            sb.Append(s);
            sb.AppendLine(Green);
        }

        sb.AppendLine();
        sb.AppendLine(GameUrl);
        _renderedGuesses = sb.ToString();
    }

    public void Clear() {
        _renderedGuesses = "";
    }
}
