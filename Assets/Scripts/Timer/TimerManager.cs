using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : BaseManager
{
    [SerializeField] private TimerRenderer Renderer;
    [SerializeField, Range(10,90)] private float GameDuration;
    [SerializeField, Range(0, 20)] private float TimeIncrement;

    private float _timeLeft;
    public float TimeLeft
    {
        get => _timeLeft;
        private set
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
        TimeLeft -= deltaTime;
    }

    public void GuessSubmitted(AnnotatedWord guess)
    {
        if (guess.Word == "BLITZ")
            S = State.Started;
        else if (guess.Correct)
            TimeLeft += TimeIncrement;
    }

    public override void ResetManager()
    {
        S = State.Waiting;
        TimeLeft = GameDuration;
    }
}
