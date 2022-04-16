using UnityEngine;

public struct MoveParameters : IAnimationParameters
{
    public float Duration { get; set; }
    public Vector3 Start { get; set; }
    public Vector3 Target { get; set; }
    public AnimationCurve Curve { get; set; }
    public MoveParameters(float duration, Vector3 start, Vector3 target, AnimationCurve curve)
    {
        Duration = duration;
        Start = start;
        Target = target;
        Curve = curve;
    }
}

public class Move : BaseAnimation<MoveParameters, Move>
{
    protected override void Animate(float t)
    {
        transform.position = Vector3.Lerp(Parameters.Start, 
            Parameters.Target, Mathf.Clamp01(Parameters.Curve.Evaluate(t)));
    }
}
