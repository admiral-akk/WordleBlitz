using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : BaseManager
{
    [SerializeField] private Canvas EndScreen;
    [SerializeField] private TextMeshProUGUI Words;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private ShareStringRenderer Share;

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
        Share.Initialize();
        yield break;
    }

    public void GameOver(float timeTaken, List<Tuple<Word, int>> guessesRequired, List<AnnotatedWord> history)
    {
        var time = TimeSpan.FromSeconds(timeTaken);
        Share.RenderGuesses(time, history, guessesRequired);
        Score.text = string.Format("Time: {0}:{1:00.}", time.Minutes, time.Seconds);
        Words.text = "";
        foreach (var (word, count) in guessesRequired)
        {
            Words.text += string.Format("{0}: {1}\n", word, count);
        }
        S = State.GameOver;
    }

    public override void ResetManager()
    {
        S = State.InProgress;
    }
}
