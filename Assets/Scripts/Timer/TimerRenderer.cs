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

    private IEnumerator HideIncrement()
    {
        yield return new WaitForSeconds(IncrementFadeout);
        Increment.gameObject.SetActive(false);
    }

    public void IncrementTime(float increment)
    {
        Increment.gameObject.SetActive(true);
        Increment.text = string.Format("+{0:00.00}", Mathf.Max(increment, 0));
        Pop.AddAnimation(Increment.gameObject, new PopParameters(0.2f, 0.3f));
        Fadeout.AddAnimation(Increment.gameObject, new FadeoutParameters(IncrementFadeout, IncrementFadeout / 2, Increment));
        StartCoroutine(HideIncrement());
    }
}
