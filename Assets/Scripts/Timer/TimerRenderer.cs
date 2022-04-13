using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI Increment;
    [SerializeField] private Transform IncrementStart;
    [SerializeField] private AnimationCurve IncrementMove;
    [SerializeField, Range(0.5f, 2f)] private float IncrementFadeout;

    public void SetRemainingSeconds(float secondsLeft)
    {
        Timer.text = string.Format("{0:000.00}", Mathf.Max(secondsLeft, 0));
    }

    private void Awake()
    {
        Increment.enabled = false;
    }

    public void IncrementTime(float increment)
    {
        if (increment == 0f)
            return;
        Increment.text = string.Format("+{0:00.00}", increment);
        Pop.AddAnimation(Increment.gameObject, new PopParameters(0.2f, 0.3f));
        Fadeout.AddAnimation(Increment.gameObject, new FadeoutParameters(IncrementFadeout, IncrementFadeout / 2, Increment));
        Move.AddAnimation(Increment.gameObject, new MoveParameters(IncrementFadeout, IncrementStart.position, Timer.transform.position, IncrementMove));
    }
}
