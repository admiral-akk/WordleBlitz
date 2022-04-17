using System;
using TMPro;
using UnityEngine;

public class TimerRenderer : BaseRenderer<float> {
    [SerializeField] private TextMeshProUGUI Timer;
    public override void Render(float data) {
        var time = TimeSpan.FromSeconds(data);
        Timer.text = string.Format("{0}:{1:00.}", time.Minutes, time.Seconds);
    }
}
