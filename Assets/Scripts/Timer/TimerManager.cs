using System.Collections;
using UnityEngine;

public class TimeUpdate : BaseUpdate<TimeUpdate> {
    public float Time;
    public TimeUpdate(float time) {
        Time = time;
    }
}
public class TimerManager : MonoBehaviour, 
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

    private float _timeSpent;
    public float TimeSpent {
        get => _timeSpent;
        private set {
            _timeSpent = value;
            new TimeUpdate(_timeSpent).Emit(gameObject);
        }
    }

    public void Awake()
    {
        S = State.None;
        TimeSpent = 0;
    }

    private void FixedUpdate()
    {
        if (S != State.Started)
            return;
        TimeSpent += Time.fixedDeltaTime;
    }

    public void Handle(GuessAnnotated update) {
        if (update.AnnotatedGuess.Word == "BLITZ")
            S = State.Started;
    }

    public void Handle(GameOver update) {
        S = State.Paused;
    }
}
