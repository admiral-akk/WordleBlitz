using System;
using TMPro;
using UnityEngine;


public class TimerRenderer : BaseRenderer<TimerData> {
    [SerializeField] private TextMeshProUGUI Timer;
    protected override void Render(TimerData data) {
        var time = TimeSpan.FromSeconds(data.TimeSpent);
        Timer.text = string.Format("{0}:{1:00.}", time.Minutes, time.Seconds);
    }
}
