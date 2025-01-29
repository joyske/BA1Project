using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CargoPlacement : MonoBehaviour
{

    private List<GameObject> placedCargo = new();

    public int Placement(GameObject prefab, Vector3 pos)
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
            if (obj != null)
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

    /// <summary>
    /// Destroys all placed items
    /// </summary>
    public void DestroyAllCargo()
    {

        for (int i = 0; i < placedCargo.Count; i++)
        {
            if (placedCargo[i] != null)
            {
                Destroy(placedCargo[i]);
                placedCargo[i] = null;
            }
        }

    }

    /// <summary>
    /// Adds an object to the list TODO this method 
    /// </summary>
    /// <param name="cargo"></param>
    public void AddCargoToList(GameObject cargo, int index)
    {
        placedCargo[index] = cargo;
    }


    /// <summary>
    /// Destroy and nulls object with index gameObjectIndex
    /// </summary>
    /// <param name="gameObjectIndex"></param>
    public void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedCargo.Count <= gameObjectIndex || placedCargo[gameObjectIndex] == null)
        {
            return;
        }
        Destroy(placedCargo[gameObjectIndex]);
        placedCargo[gameObjectIndex] = null;    

    }
}
