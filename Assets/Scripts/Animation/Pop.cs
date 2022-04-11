using UnityEngine;

public class Pop : BaseAnimation
{
    private float _animationLength = 0.2f;
    private float _animationMagnitude = 0.3f;

    protected override float SetDuration()
    {
        return _animationLength;
    }

    protected override void ApplyAnimation(float t)
    {
        transform.localScale = Vector3.one * (1 + _animationMagnitude*(1- Mathf.Abs(2*t-1)));
    }
}
