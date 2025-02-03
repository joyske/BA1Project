using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsToggle : MonoBehaviour
{
    /// <summary>
    /// Enables physics for the given object.
    /// </summary>
    /// <param name="obj"></param>
    public static void EnablePhysics(GameObject obj)
    {
        if ( obj != null)
        {
            
        
            // enable physics simulation
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            if (obj.GetComponent<SphereCollider>())
            {
                obj.GetComponent<BoxCollider>().enabled = false;
                obj.GetComponent<SphereCollider>().enabled = true;
            }

            if (obj.GetComponent<MeshCollider>())
            {

                if (obj.GetComponent<BoxCollider>())
                {
                    obj.GetComponent<BoxCollider>().enabled = false;
                    obj.GetComponent<MeshCollider>().enabled = true;
                }

            }
        }
    }
}
