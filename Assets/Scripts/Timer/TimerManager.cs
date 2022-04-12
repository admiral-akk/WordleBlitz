using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : BaseManager
{
    [SerializeField] private TimerRenderer Renderer;
    [SerializeField, Range(10,90)] private float GameDuration;

    private float _timeLeft;
    private float TimeLeft
    {
        get => _timeLeft;
        set
        {
            _timeLeft = value;
            Renderer.SetRemainingSeconds(_timeLeft);
        }
    }

    public override IEnumerator Initialize()
    {
        TimeLeft = GameDuration;
        yield break;
    }

    public void DecrementTime(float deltaTime)
    {
        TimeLeft = TimeLeft - deltaTime;
    }
}
