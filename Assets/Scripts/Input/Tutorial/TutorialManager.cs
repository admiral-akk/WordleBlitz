using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Button Help;
    [SerializeField] private Canvas Tutorial;
    [SerializeField, Range(0,3)] private float RecentlyOpenDelay;

    private void Awake()
    {
        Help.onClick.AddListener(OpenTutorial);
        OpenTutorial();
    }

    private void OpenTutorial()
    {
        StartCoroutine(SetOpen());
    }

    private IEnumerator SetOpen()
    {
        S = State.RecentlyOpen;
        yield return new WaitForSeconds(RecentlyOpenDelay);
        S = State.Open;
    }

    private IEnumerator CloseTutorial()
    {
        S = State.RecentlyClosed;
        yield return new WaitForSeconds(RecentlyOpenDelay);
        S = State.Closed;
    }

    private enum State
    {
        RecentlyOpen,
        Open,
        RecentlyClosed,
        Closed,
    }

    private State _s;
    private State S
    {
        get => _s;
        set
        {
            switch (value)
            {
                case State.RecentlyOpen:
                    if (_s != State.Closed)
                        return;
                    Tutorial.gameObject.SetActive(true);
                    break;
                case State.Open:
                    if (_s != State.RecentlyOpen)
                        return;
                    Tutorial.gameObject.SetActive(true);
                    break;
                case State.RecentlyClosed:
                    if (_s != State.Open)
                        return;
                    Tutorial.gameObject.SetActive(false);
                    break;
                case State.Closed:
                    if (_s != State.RecentlyClosed)
                        return;
                    Tutorial.gameObject.SetActive(false);
                    break;
            }
            _s = value;
        }
    }

    private void OnGUI()
    {
        if (S != State.Open)
            return;
        var e = Event.current;
        if (e == null)
            return;
        if (e.isMouse && e.button == 0)
            StartCoroutine(CloseTutorial());
        if (e.type != EventType.KeyDown)
            return;
        if (e.keyCode == KeyCode.KeypadEnter || e.keyCode == KeyCode.Return)
            StartCoroutine(CloseTutorial());
    }
}
