using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject PlayerRef;
    Vector3 Offset;

    private void Start()
    {
        Offset = transform.position;
    }

    void Update()
    {
        transform.position = PlayerRef.transform.position + Offset;
        
        
    }
}
