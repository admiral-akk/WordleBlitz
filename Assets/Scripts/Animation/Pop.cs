using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    private float _animationLength = 0.15f;
    private float _animationMagnitude = 0.3f;
    private float _timeLeft;
    private void Awake()
    {
        if (GetComponents<Pop>().Length > 1)
            Destroy(this);
        _timeLeft = _animationLength;
    }

    private void UpdateScale(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        UpdateScale(1 + _animationMagnitude*(1-Mathf.Abs(_animationLength / 2f - _timeLeft) / (_animationLength / 2f)));
        if (_timeLeft < 0)
            Destroy(this);
    }
    private void OnDestroy()
    {
        UpdateScale(1);
    }
}
