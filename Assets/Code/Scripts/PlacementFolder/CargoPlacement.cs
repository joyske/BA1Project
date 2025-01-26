using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CargoPlacement : MonoBehaviour
{
    // for inspector might delete later
    [SerializeField]
    private List<GameObject> placedCargo = new();

    internal int Placement(GameObject prefab, Vector3 pos)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = pos;
        placedCargo.Add(newObject);

        return placedCargo.Count - 1; // null values werden nicht gelöscht 
    }

    /// <summary>
    /// Turns on physics for each placed Object
    /// </summary>
    public void EnablePhysics()
    {
        foreach (GameObject obj in placedCargo)
        {
            // enable physics simulation
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            // toggle colliders for physics simulation TODO cylinderC
            SphereCollider sphereCollider = obj.GetComponent<SphereCollider>();
            if (sphereCollider != null)
            {
                BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    boxCollider.enabled = false;
                }
                sphereCollider.enabled = true;

            }
        }
    }


    /// <summary>
    /// Destroys all placed items
    /// </summary>
    public void DestroyAllCargo()
    {
        foreach (GameObject obj in placedCargo)
        {
            Destroy(obj);
        }

        placedCargo.Clear();
    }


    /// <summary>
    /// Adds an object to the list TODO this method 
    /// </summary>
    /// <param name="cargo"></param>
    public void AddCargoToList(GameObject cargo)
    {
        placedCargo.Add(cargo);
    }

    internal void RemoveObjectAt(int gameObjectindex)
    {
        if (placedCargo.Count <= gameObjectindex || placedCargo[gameObjectindex] == null)
            return;
        Destroy(placedCargo[gameObjectindex]);
        placedCargo[gameObjectindex] = null;    
    }
}
