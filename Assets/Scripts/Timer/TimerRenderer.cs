using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI Increment;
    [SerializeField, Range(0.5f, 2f)] private float IncrementFadeout;

    public void SetRemainingSeconds(float secondsLeft)
    {
        Timer.text = string.Format("{0:00.00}", Mathf.Max(secondsLeft, 0));
    }

    public void IncrementTime(float increment)
    {
    }
}
