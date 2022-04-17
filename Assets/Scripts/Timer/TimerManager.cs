using System.Collections;
using UnityEngine;

public class TimerManager : NewBaseManager<TimeData, TimeUpdate>, 
    IUpdateObserver<GuessAnnotated>,
    IUpdateObserver<GameOver>
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

    public void Awake()
    {
        S = State.None;
        UpdateData(Data.Reset());
    }

    private void FixedUpdate()
    {
        if (S != State.Started)
            return;
        UpdateData(Data.Increment(Time.fixedDeltaTime));
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

    public void Handle(GameOver update) {
        S = State.Paused;
    }

    public float TimeLeft => Data.Time;
}
