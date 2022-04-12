using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : BaseManager
{
    [SerializeField] private TimerRenderer Renderer;
    [SerializeField, Range(10,90)] private float GameDuration;

    private float _timeLeft;
    private float TimeLeft
    {
        get => _timeLeft;
        set
        {
            _timeLeft = value;
            Renderer.SetRemainingSeconds(_timeLeft);
        }
    }

    private enum State
    {
        Waiting, 
        Started
    }

    private State S
    {
        get;
        set;
    }

    public override IEnumerator Initialize()
    {
        S = State.Waiting;
        TimeLeft = GameDuration;
        yield break;
    }

    public void DecrementTime(float deltaTime)
    {
        if (S != State.Started)
            return;
        TimeLeft = TimeLeft - deltaTime;
    }

    public void GuessSubmitted(Word guess)
    {
        if (guess == "BLITZ")
            S = State.Started;
    }
}
