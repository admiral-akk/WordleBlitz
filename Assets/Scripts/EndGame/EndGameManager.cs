using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour, IUpdateObserver<GameOver>
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

    private void Awake()
    {
        S = State.InProgress;
    }

    public void Handle(GameOver update) {
        var guessesRequired = GetComponent<KnowledgeManager>().GuessesRequired;
        var history = GetComponent<HistoryManager>().GetHistory();
        var time = TimeSpan.FromSeconds(GetComponent<TimerManager>().TimeSpent);

        Share.RenderGuesses(time, history, guessesRequired);
        Score.text = string.Format("Time: {0}:{1:00.}", time.Minutes, time.Seconds);
        Words.text = "";
        foreach (var (word, count) in guessesRequired) {
            Words.text += string.Format("{0}: {1}\n", word, count);
        }
        S = State.GameOver;
    }
}
