using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraAttachPoint;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    [Header("RayCast")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxLength = 300;
    
    private Transform _transform;
    private float _verticalAngle = 0;
    private float _horizontalAngle = 0;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        _transform.position = _cameraAttachPoint.position;
        _transform.rotation = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0);
    }

    public void Rotate(float inputX, float inputY)
    {
        _horizontalAngle += inputX;
        _verticalAngle -= inputY;
        _verticalAngle = Mathf.Clamp(_verticalAngle, _minAngle, _maxAngle);
    }

    public Vector3 RayCast()
    {
        var forward = _cameraTransform.forward;

        if (Physics.Raycast(_cameraTransform.position, forward, out RaycastHit hitInfo, _maxLength, _layerMask))
        {
            return hitInfo.point;
        }

        return _cameraTransform.position + forward * _maxLength;
    }
}
