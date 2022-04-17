using System;
using TMPro;
using UnityEngine;

public class TimerRenderer : NewBaseRenderer<TimeUpdate>
{
    [SerializeField] private TextMeshProUGUI Timer;

    public override void Handle(TimeUpdate update) {
        var time = TimeSpan.FromSeconds(update.Time);
        Timer.text = string.Format("{0}:{1:00.}", time.Minutes, time.Seconds);
    }
}
