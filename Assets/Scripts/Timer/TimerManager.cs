using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : NewBaseManager<TimeData, TimeUpdate>
{
    private enum State
    {
        None,
        Paused,
        Started,
    }

    private State S
    {
        get;
        set;
    }

    private TimeData _data;
    protected override TimeData Data {
        get => _data ??= new TimeData();
    }

    public override IEnumerator Initialize()
    {
        S = State.None;
        UpdateData(Data.Reset());
        yield break;
    }

    private void FixedUpdate()
    {
        if (S != State.Started)
            return;
        UpdateData(Data.Increment(Time.fixedDeltaTime));
    }

    // Should be a "game started" listener.
    public void GuessSubmitted(AnnotatedWord guess)
    {
        if (guess.Word == "BLITZ")
            S = State.Started;
    }

    public void GameOver() {
        S = State.Paused;
    }

    public override void ResetManager()
    {
        S = State.Paused;
        UpdateData(Data.Reset());
    }
    public float TimeLeft => Data.Time;
}
