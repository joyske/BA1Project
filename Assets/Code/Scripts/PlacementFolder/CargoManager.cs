using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UIElements;

public class CargoManager : MonoBehaviour
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
    /// Turns on physics for each placed Item
    /// </summary>
    public void SimulatePhysics()
    {
        foreach (GameObject obj in placedCargo)
        {
            PhysicsToggle.EnablePhysics(obj);           
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
    /// Adds an item to the list 
    /// </summary>
    /// <param name="item"></param>
    public void AddItemToList(GameObject item, int index)
    {
        placedCargo[index] = item;
    }


    /// <summary>
    /// Destroy and nulls object with index itemIndex
    /// </summary>
    /// <param name="itemIndex"></param>
    public void RemoveObjectAt(int itemIndex)
    {
        if (placedCargo.Count <= itemIndex || placedCargo[itemIndex] == null)
        {
            return;
        }
        Destroy(placedCargo[itemIndex]);
        placedCargo[itemIndex] = null;    
    }
}
