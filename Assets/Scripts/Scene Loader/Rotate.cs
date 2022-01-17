using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Range(0, 360)]
    public float RotationPerSecond;

    void Update()
    {
        transform.Rotate(Vector3.forward, RotationPerSecond * Time.deltaTime);    
    }
}
