using System;
using UnityEngine;

public class GenericAnimation : MonoBehaviour
{

    private float _time;
    private float _duration;
    private float Duration
    {
        get => _duration;
        set
        {
            _duration = value;
            Destroy(this, _duration);
        }
    }
    private Action<float> _animation;
    private Action _terminate;

    public static void AddAnimation(GameObject target, Action<float> animationCallback, float duration, Action terminationCallback)
    {
       var animation = (GenericAnimation)target.AddComponent(typeof(GenericAnimation));
        animation._terminate = terminationCallback;
        animation._animation = animationCallback;
        animation._duration = duration;
        animation._time = 0f;
    }

    private void OnDestroy()
    {
        if (_terminate != null)
            _terminate();
        else
            _animation(1f);
    }

    private void Update()
    {
        _time += Time.deltaTime;
        _animation(_time / _duration);
    }

}
