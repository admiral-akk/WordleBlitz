using UnityEngine;

public abstract class ParameterizedAnimation<T, AnimationType> : MonoBehaviour where T : IAnimationParameters where AnimationType : ParameterizedAnimation<T, AnimationType>
{
    private float _duration;
    private float _time;
    private enum State
    {
        None,
        Started,
    }
    private State _state;

    protected abstract void Animate(float t);
    public void Initialize(T parameters)
    {
        SetParameters(parameters);
        _time = 0f;
        _duration = parameters.Duration;
        _state = State.Started;
    }
    protected abstract void SetParameters(T parameters);

    private void Update()
    {
        if (_state == State.None)
            return;
        _time += Time.deltaTime;
        if (_time > _duration)
            Destroy(this);
        else
            Animate(_time / _duration);
    }
    private void OnDestroy()
    {
        Animate(1f);
    }

    public static void AddAnimation(GameObject target, T parameters)
    {
        var existingAnimation = target.GetComponent<AnimationType>();
        if (existingAnimation != null)
            Destroy(existingAnimation);
        var animation = (AnimationType)target.AddComponent(typeof(AnimationType));
        animation.Initialize(parameters);
    }
}