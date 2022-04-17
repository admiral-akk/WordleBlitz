using UnityEngine;
public class TimerManager : RendererManager<float>,
    IUpdateObserver<GuessAnnotated>,
    IUpdateObserver<GameOver> {
    private enum State {
        None,
        Paused,
        Started,
    }

    private State S;

    public float TimeSpent { get; private set; }

    protected override float Data => TimeSpent;

    public void Awake() {
        S = State.None;
        TimeSpent = 0;
    }

    private void FixedUpdate() {
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
