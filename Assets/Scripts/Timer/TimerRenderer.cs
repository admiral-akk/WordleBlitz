using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TimerRenderer;

public class TimerRenderer : NewBaseRenderer<State>
{
    public enum State
    {
        None,
        Paused,
        Running
    }

    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI Increment;
    [SerializeField] private Transform IncrementStart;
    [SerializeField] private AnimationCurve IncrementMove;
    [SerializeField, Range(0.5f, 2f)] private float IncrementFadeout;

    public void SetRemainingSeconds(float secondsLeft)
    {
        var time = TimeSpan.FromSeconds(secondsLeft);
        Timer.text = string.Format("{0}:{1:00.}", time.Minutes, time.Seconds);
    }

    private void Awake()
    {
        RenderState = State.Paused;
        Increment.enabled = false;
    }

    protected override void StateChanged(State oldState, State newState)
    {
        switch (newState)
        {
            case State.None:
                break;
            case State.Paused:
                Increment.enabled = false;
                break;
            case State.Running:
                Increment.enabled = false;
                break;
        }
    }

    public void IncrementTime(float increment)
    {
        if (increment == 0f)
            return;
        Increment.text = string.Format("+{0:00.}", increment);
        Pop.AddAnimation(Increment.gameObject, new PopParameters(0.2f, 0.3f));
        Fadeout.AddAnimation(Increment.gameObject, new FadeoutParameters(IncrementFadeout, IncrementFadeout / 2, Increment));
        Move.AddAnimation(Increment.gameObject, new MoveParameters(IncrementFadeout, IncrementStart.position, Timer.transform.position, IncrementMove));
    }
}
