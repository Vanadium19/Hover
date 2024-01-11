using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HoverControlsInfo
{
    public event Action ShootPressed;
    public Vector3 MoveInput;
    public Vector3 AimingPoint;
    public Vector3 LookPoint;
}
