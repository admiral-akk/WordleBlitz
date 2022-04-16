using UnityEngine;

public abstract class BaseAnimation<ParameterType, AnimationType> : MonoBehaviour 
    where ParameterType : IAnimationParameters 
    where AnimationType : BaseAnimation<ParameterType, AnimationType>
{
    private float _time;
    private void Initialize(ParameterType parameters)
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

    protected ParameterType Parameters;
    protected abstract void Animate(float t);

    public static void AddAnimation(GameObject target, ParameterType parameters)
    {
        var existingAnimation = target.GetComponent<AnimationType>();
        if (existingAnimation != null)
            Destroy(existingAnimation);
        var animation = (AnimationType)target.AddComponent(typeof(AnimationType));
        animation.Initialize(parameters);
    }
}