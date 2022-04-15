using UnityEngine;

public abstract class ParameterizedAnimation<T, AnimationType> : MonoBehaviour where T : IAnimationParameters where AnimationType : ParameterizedAnimation<T, AnimationType>
{
    private float _time;
    private void Initialize(T parameters)
    {
        Parameters = parameters;
        _time = 0f;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > Parameters.Duration)
            Destroy(this);
        else
            Animate(_time / Parameters.Duration);
    }
    private void OnDestroy()
    {
        Animate(1f);
    }

    protected T Parameters;
    protected abstract void Animate(float t);

    public static void AddAnimation(GameObject target, T parameters)
    {
        var existingAnimation = target.GetComponent<AnimationType>();
        if (existingAnimation != null)
            Destroy(existingAnimation);
        var animation = (AnimationType)target.AddComponent(typeof(AnimationType));
        animation.Initialize(parameters);
    }
}