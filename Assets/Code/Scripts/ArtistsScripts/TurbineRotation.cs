using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbineRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
