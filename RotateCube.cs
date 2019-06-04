using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : Trap
{
    [SerializeField]
    float rotateX;

    [SerializeField]
    float rotateY;

    [SerializeField]
    float rotateZ;

    protected override void Update()
    {
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime, Space.World);
    }
}