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

public class Pop : ParameterizedAnimation<PopParameters, Pop>
{
    private float _magnitude;


    protected override void Animate(float t)
    {
        transform.localScale = Vector3.one * (1 + _magnitude * (1 - Mathf.Abs(2 * t - 1)));
    }

    protected override void SetParameters(PopParameters parameters)
    {
        _magnitude = parameters.Magnitude;
    }
}
