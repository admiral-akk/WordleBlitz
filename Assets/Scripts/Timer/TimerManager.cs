using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : NewBaseManager<TimeUpdate>
{
    [SerializeField] private TimerRenderer Renderer;
    [SerializeField, Range(10, 180)] private float GameDuration;
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

    private TimeData _data;
    protected override NewBaseData<TimeUpdate> Data {
        get => _data ??= new TimeData();
    }

    public override IEnumerator Initialize()
    {
        S = State.Waiting;
        TimeLeft = 0;
        yield break;
    }

    public void UpdateTime(float deltaTime)
    {
        if (S != State.Started)
            return;
        TimeLeft += deltaTime;
    }

    public void IncrementTime()
    {
        TimeLeft += TimeIncrement;
        TimeLeft = Mathf.Min(GameDuration - 0.01f, TimeLeft);
        Renderer.IncrementTime(TimeIncrement);
    }

    public void GuessSubmitted(AnnotatedWord guess)
    {
        if (guess.Word == "BLITZ")
            S = State.Started;
    }

    public override void ResetManager()
    {
        S = State.Waiting;
        TimeLeft = GameDuration;
    }
}
