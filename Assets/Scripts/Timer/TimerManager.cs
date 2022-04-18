using UnityEngine;
using static TimerData;

public class TimerData : BaseRenderData<TimerData> {
    public enum State {
        None,
        Paused,
        Started,
    }

    private State _state;
    private float _timeSpent;

    public State S {
        get => _state;
        set {
            if (_state == value)
                return;
            _state = value;
            HasUpdate = true;
        }
    }

    public float TimeSpent {
        get => _timeSpent;
        set {
            if (_timeSpent == value)
                return;
            _timeSpent = value;
            HasUpdate = true;
        }
    }

    public TimerData() : base() {
        S = State.None;
        TimeSpent = 0f;
    }
}


public class TimerManager : RendererManager<TimerData>,
    IUpdateObserver<GuessAnnotated>,
    IUpdateObserver<GameOver> {

    public float TimeSpent { get => _data.TimeSpent; set => _data.TimeSpent = value; }

    private TimerData _data;
    protected override TimerData Data {
        get {
            if (_data == null)
                _data = new TimerData();
            return _data;
        }
    }

    private void FixedUpdate() {
        if (Data.S != State.Started)
            return;
        Data.TimeSpent += Time.fixedDeltaTime;
    }

    public void Handle(GuessAnnotated update) {
        if (update.AnnotatedGuess.Word == "BLITZ")
            Data.S = State.Started;
    }

    public void Handle(GameOver update) {
        Data.S = State.Paused;
    }
}
