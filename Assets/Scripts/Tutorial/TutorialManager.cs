using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialData : BaseRenderData<TutorialData> {
    public enum State {
        Start,
        Open,
        Closed,
    }

    private State _state;

    public State S {
        get => _state;
        set {
            if (_state == value)
                return;
            _state = value;
            HasUpdate = true;
        }
    }

    public TutorialData() : base() {
        S = State.Start;
    }
}


public class TutorialManager : RendererManager<TutorialData>, 
    IUpdateObserver<PlayerInput>,
    IUpdateObserver<GuessAnnotated> {
    private TutorialData _data;
    protected override TutorialData Data {
        get {
            if (_data == null)
                _data = new TutorialData();
            return _data;
        }
    }

    public void Handle(PlayerInput update) {
        if (TutorialState == TutorialData.State.Start)
            return;
        CloseTutorial();
    }
    public void Handle(GuessAnnotated update) {
        if (TutorialState != TutorialData.State.Start)
            return;
       CloseTutorial();
    }
    private enum State {
        Start,
        RecentlyChanged,
        CanInteract,
    }
    private State _s;
    private State S { get => _s; set {
            _s = value;
            if (_s == State.RecentlyChanged)
                StartCoroutine(RecentlyChanged());
        }
    }

    private TutorialData.State TutorialState {
        get => Data.S;
        set {
            if (S == State.RecentlyChanged)
                return;
            S = State.RecentlyChanged;
            Data.S = value;
        }
    }
    [SerializeField, Range(0,3)] private float RecentlyOpenDelay;
    [SerializeField] private Button Help;


    private void Awake()
    {
        Help.onClick.AddListener(OpenTutorial);
        Data.S = TutorialData.State.Start;
        S = State.Start;
    }

    private IEnumerator RecentlyChanged() {
        yield return new WaitForSeconds(RecentlyOpenDelay);
        S = State.CanInteract;
    }

    private void OpenTutorial() {
        TutorialState = TutorialData.State.Open;
    }
    private void CloseTutorial() {
        TutorialState = TutorialData.State.Closed;
    }
    private void OnGUI() {
        if (TutorialState != TutorialData.State.Open)
            return;
        var e = Event.current;
        if (e == null)
            return;
        if (e.isMouse && e.button == 0)
            CloseTutorial();
    }
}
