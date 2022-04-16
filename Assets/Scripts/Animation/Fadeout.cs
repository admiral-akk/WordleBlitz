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
    public FadeoutParameters(float duration, float startTime, Graphic colorObject)
    {
        Duration = duration;
        _startTime = startTime;
        ColorObjects = new Graphic[] { colorObject };
    }
}

public class Fadeout : BaseAnimation<FadeoutParameters, Fadeout> {
    protected override void Animate(float t)
    {
        float alpha = t < Parameters.Start ? 1f : 1 - (t - Parameters.Start) / (1 - Parameters.Start);
        foreach (var graphic in Parameters.ColorObjects)
        {
            graphic.enabled = alpha > 0f;
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }
}
