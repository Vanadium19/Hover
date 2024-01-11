using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IHoverControls
{
    [Range(1, 5)]
    [SerializeField] private float _sensitivity;
    [SerializeField] private FreeLookCamera _freeLookCamera;

    private HoverControlsInfo _hoverControlsInfo = new HoverControlsInfo();

    public HoverControlsInfo GetControls()
    {
        return _hoverControlsInfo;
    }

    private void Update()
    {
        _freeLookCamera.Rotate(Input.GetAxis("Mouse X") * _sensitivity, Input.GetAxis("Mouse Y") * _sensitivity);
        _hoverControlsInfo.MoveInput = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
        _hoverControlsInfo.LookPoint = _hoverControlsInfo.AimingPoint = _freeLookCamera.RayCast();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawSphere(_hoverControlsInfo.LookPoint, 1);
    //}
}
