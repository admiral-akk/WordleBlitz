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

public class Move : ParameterizedAnimation<MoveParameters, Move>
{
    private Vector3 _start;
    private Vector3 _target;
    private AnimationCurve _curve;

    protected override void Animate(float t)
    {
        transform.position = Vector3.Lerp(_start, _target, Mathf.Clamp01(_curve.Evaluate(t)));
    }

    protected override void SetParameters(MoveParameters parameters)
    {
        _start = parameters.Start;
        _target = parameters.Target;
        _curve = parameters.Curve;
    }
}
