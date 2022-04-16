using UnityEngine;

public struct PopParameters : IAnimationParameters
{
    public float Duration { get; set; }
    public float Magnitude { get; set; }
    public PopParameters(float duration, float magnitude)
    {
        Duration = duration;
        Magnitude = magnitude;
    }
}

public class Pop : BaseAnimation<PopParameters, Pop>
{
    protected override void Animate(float t)
    {
        float size =  1 + Parameters.Magnitude * (1 - Mathf.Abs(2 * t - 1));
        transform.localScale = Vector3.one * size;
    }
}
