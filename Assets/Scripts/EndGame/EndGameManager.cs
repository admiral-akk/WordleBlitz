using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EndGameManager : BaseManager
{
    [SerializeField] private Canvas EndScreen;
    [SerializeField] private TextMeshProUGUI Words;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI NextWord;

    private enum State
    {
        InProgress,
        GameOver,
    }

    private State S
    {
        set {
            switch (value)
            {
                case State.InProgress:
                    EndScreen.gameObject.SetActive(false);
                    break;
                case State.GameOver:
                    EndScreen.gameObject.SetActive(true);
                    break;
            }
        }
    }

    public override IEnumerator Initialize()
    {
        S = State.InProgress;
        yield break;
    }

    public void GameOver(List<Word> successful, Word nextAnswer)
    {
        if (successful.Count < 1)
            Score.text = string.Format("You didn't get any words!", successful.Count);
        if (successful.Count == 1)
            Score.text = string.Format("You got a word!", successful.Count);
        if (successful.Count > 1)
            Score.text = string.Format("You got {0} words!", successful.Count);
        if (successful.Count == 0){ 
            Words.text = ""; 
        }
        if (successful.Count > 0)
        {
            var wordList = successful
                .GetRange(1, successful.Count - 1)
                .Select(w => w.ToString())
                .Aggregate("", (s1, acc) => s1 + ", " + acc);
            Words.text = wordList.Substring(2, wordList.Length - 2);
        }

        NextWord.text = string.Format("Next word: '{0}'", nextAnswer);
        S = State.GameOver;
    }

    public override void ResetManager()
    {
        S = State.InProgress;
    }
}
