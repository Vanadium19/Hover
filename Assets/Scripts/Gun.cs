using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _verticalAxis;
    [SerializeField] private Transform _horizontalAxis;
    [SerializeField] private PidRegulator _xAxisPidRegulator = new PidRegulator();
    [SerializeField] private PidRegulator _yAxisPidRegulator = new PidRegulator();
    [SerializeField] private Transform _hoverTransform;
    [SerializeField] private float _verticalAimingSpeed = 35;

    private Vector3 _targetPoint;

    public void SetTarget(Vector3 target)
    {
        _targetPoint = target;
    }

    private void Update()
    {
        Vector3 hoverForward = _hoverTransform.forward;
        Vector3 forward = _verticalAxis.forward;
        Vector3 up = _verticalAxis.up;
        Vector3 aimDirection = (_targetPoint - _verticalAxis.position).normalized;

        float verticalAngle = Vector3.SignedAngle(hoverForward, forward, up);
        float needAngle = verticalAngle + Vector3.SignedAngle(forward, aimDirection, up);

        _verticalAxis.rotation = Quaternion.AngleAxis(_yAxisPidRegulator.Tick(Time.deltaTime, verticalAngle, needAngle) 
            * Time.deltaTime * _verticalAimingSpeed, up) * _verticalAxis.rotation;
    }
}
