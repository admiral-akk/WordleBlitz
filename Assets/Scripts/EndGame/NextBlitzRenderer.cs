using System;
using TMPro;
using UnityEngine;

public class NextBlitzRenderer : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    void Update() {
        var timeToTomorrow = DateTime.Today.AddDays(1) - DateTime.Now;
        text.text = string.Format("{0:00.}:{1:00.}:{2:00.}", timeToTomorrow.Hours, timeToTomorrow.Minutes, timeToTomorrow.Seconds);
    }
}
