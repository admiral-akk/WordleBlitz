using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Timer;

    public void SetRemainingSeconds(float secondsLeft)
    {
        Timer.text = string.Format("{0:00.00}", Mathf.Max(secondsLeft, 0));
    }
}
