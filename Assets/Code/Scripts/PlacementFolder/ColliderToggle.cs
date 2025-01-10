using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderToggle : MonoBehaviour
{
    GameObject[] objectChildren;
    GameObject[] boxColliderChildren;

    void Start()
    {
        objectChildren = GameObject.FindGameObjectsWithTag("Object");
        boxColliderChildren = GameObject.FindGameObjectsWithTag("Collider");
    }

    //public static void ToggleColliders()
    //{
    //    foreach (GameObject obj in objectChildren)
    //    {
    //        SphereCollider sphereCollider = obj.GetComponent<SphereCollider>();
    //        if (sphereCollider != null)
    //        {
    //            sphereCollider.enabled = true;
    //        }
    //    }
    //}


}
