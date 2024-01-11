using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[System.Serializable]
public class PidRegulator
{
    [SerializeField] private float _ki = 1;
    [SerializeField] private float _kp = 1;
    [SerializeField] private float _kd = 1;
    [SerializeField] private float _maxOut = 1000;
    [SerializeField] private float _minOut = -1000;

    private float _integral, _prevError, _error, _currentDelta, _targetPoint;

    public PidRegulator() { }

    public PidRegulator(float ki, float kp, float kd)
    {
        _ki = ki;
        _kp = kp;
        _kd = kp;
    }

    private float ComputePid(float time, float input)
    {
        _error = _targetPoint - input;
        _integral = Mathf.Clamp(_integral + _error * time * _ki, _minOut, _maxOut);
        _currentDelta = (_error - _prevError) / time;
        _prevError = _error;
        return Mathf.Clamp(_error * _kp + _integral + _currentDelta * _kd, _minOut, _maxOut);
    }

    public float Tick(float time, float input)
    {
        return ComputePid(time, input);
    }

    public float Tick(float time, float input, float target)
    {
        SetTarget(target);
        return ComputePid(time, input);
    }

    public void SetTarget(float target)
    {
        _targetPoint = target;
    }

    public void SetMinMax(float min, float max)
    {
        _minOut = min;
        _maxOut = max;
    }
}
