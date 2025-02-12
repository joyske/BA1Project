using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static GridData;


// Save positions of objects and checking for valid placement
public class GridData : MonoBehaviour
{
    
    public Dictionary<Vector3Int, PlacementData> placedObjects = new();


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    /// <summary>
    ///  
    /// </summary>
    /// <param name="gridPosition"> Cell object would be placed on</param>
    /// <param name="objectSize"> Size of object</param>
    /// <param name="ID"> ID of object type</param>
    /// <param name="objectIndex"> </param>
    /// <exception cref="Exception"></exception>
    public void AddObjectAt(Vector3Int gridPosition, Vector3Int objectSize, int ID, int objectIndex)
    {
 
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionToOccupy, ID, objectIndex);
        foreach (var position in positionToOccupy)
        {
            // Check if cell is already occupied (position occupied) 
            if (placedObjects.ContainsKey(position))
            {
                throw new Exception($"Dictionary already contains item");
            }
            //placedObjects[position] = data;
        }
        placedObjects[gridPosition] = data;

    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector3Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var position in positionToOccupy)
        {
            if (placedObjects.ContainsKey(position) || BoundsChecker.IsOutOfBounds(position))
            {
                return false;
            }

            foreach (var placementData in placedObjects.Values)
            {
                if (placementData.occupiedPositions.Contains(position))
                {
                    return false;
                }
            }
        }
        return true;
    }

        /// <summary>
        ///  Calulate cells that an object would occupy 
        /// </summary>
        /// <param name="gridPosition"> Cell object would be placed on</param>
        /// <param name="objectSize"> Size of object </param>
        /// <returns></returns>
        private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector3Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                for (int z = 0; z< objectSize.z; z++)
                {
                    returnVal.Add(gridPosition + new Vector3Int(x, y, z));
                }
            }
        }
        return returnVal;
    }

    public void PlaceObjects()
    {
        Vector3Int pos;
        foreach (var entry in placedObjects)
        {
            pos = entry.Key;
        }
    }

    public int GetIndex(Vector3Int gridPos)
    {
        if (placedObjects.ContainsKey(gridPos) == false) 
        {
            return -1;
        } else
        {
            return placedObjects[gridPos].PlacedObjectIndex;
        }
    }

    public void RemoveObjectAt(Vector3Int gridPos)
    {
        foreach (var pos in placedObjects[gridPos].occupiedPositions)
        {
            placedObjects.Remove(pos);
        }

    }

    public int GetObjectID(Vector3Int gridPos)
    {
        return placedObjects[gridPos].ID;
    }


    /// <summary>
    ///  Data of placed object for saving and checking if positions are already occupied 
    /// </summary>
    public class PlacementData
    {
        public List<Vector3Int> occupiedPositions;

        public int ID { get; private set; }  // for saving position 

        public int PlacedObjectIndex { get; private set; }

        public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
        {
            this.occupiedPositions = occupiedPositions;
            ID = iD;
            PlacedObjectIndex = placedObjectIndex;
        }
    }
}