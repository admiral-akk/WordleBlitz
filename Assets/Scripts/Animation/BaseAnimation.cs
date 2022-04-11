using UnityEngine;

public abstract class BaseAnimation : MonoBehaviour
{
    private float _duration;
    private float _timeLeft;
    private void Awake()
    {
        if (GetComponents<BaseAnimation>().Length > 1)
            Destroy(this);
        _timeLeft = 0;
        _duration = SetDuration();
    }

    protected abstract float SetDuration();
    protected abstract void ApplyAnimation(float t);

    private void Update()
    {
        _timeLeft += Time.deltaTime;
        ApplyAnimation(_timeLeft / _duration);
       if (_timeLeft >= _duration)
            Destroy(this);
    }
    private void OnDestroy()
    {
        ApplyAnimation(1);
    }
}
