using System;
using TMPro;
using UnityEngine;

public class TimerRenderer : MonoBehaviour, IUpdateObserver<TimeUpdate>
{
    [SerializeField] private TextMeshProUGUI Timer;

    public void Handle(TimeUpdate update) {
        var time = TimeSpan.FromSeconds(update.Time);
        Timer.text = string.Format("{0}:{1:00.}", time.Minutes, time.Seconds);
    }
}
