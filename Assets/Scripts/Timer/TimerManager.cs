using System.Collections;
using UnityEngine;

public class TimerManager : NewBaseManager<TimeData, TimeUpdate>, IUpdateObserver<GuessAnnotated>
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
    public void GameOver() {
        S = State.Paused;
    }

    public override void ResetManager()
    {
        S = State.Paused;
        UpdateData(Data.Reset());
    }

    public void Handle(GuessAnnotated update) {
        if (update.AnnotatedGuess.Word == "BLITZ")
            S = State.Started;
    }

    public float TimeLeft => Data.Time;
}
