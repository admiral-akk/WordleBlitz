using UnityEngine.UI;

public struct FadeoutParameters : IAnimationParameters
{
    public float Duration { get; set; }
    private float _startTime;
    public float Start => _startTime / Duration;
    public Graphic[] ColorObjects { get; }
    public FadeoutParameters(float duration, float startTime, Graphic[] colorObjects)
    {
        Duration = duration;
        _startTime = startTime;
        ColorObjects = colorObjects;
    }
}

public class Fadeout : ParameterizedAnimation<FadeoutParameters, Fadeout>
{
    private float _start;

    private Graphic[] _colorObjects;

    protected override void Animate(float t)
    {
        float alpha = t < _start ? 1f : 1 - (t - _start) / (1 - _start);
        foreach (var graphic in _colorObjects)
        {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }

    protected override void SetParameters(FadeoutParameters parameters)
    {
        _start = parameters.Start;
        _colorObjects = parameters.ColorObjects;
    }
}
