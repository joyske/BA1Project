using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class InventorySceneData : ScriptableObject
{
    public List<InventoryCountData> SceneData = new List<InventoryCountData>();
}

[Serializable]
public class InventoryCountData
{
    [field: SerializeField]
    public int SceneID { get; private set; }

    [field: SerializeField]
    public List<CountData> CountData { get; private set; } = new List<CountData>();
}

[Serializable]
public class CountData
{

        [field: SerializeField]
        public int ItemID { get; private set; }

        [field: SerializeField]
        public int TotalItemCount { get; private set; } 

        [field: SerializeField]
        public int PlacedItemCount { get; private set; } 

        public int RemainingItemCount => TotalItemCount - PlacedItemCount; 

        // Method to increment placed count
        public void IncrementPlacedCount()
        {
            if (RemainingItemCount > 0)
            {
                PlacedItemCount++;
            }
            else
            {
                Debug.LogError($"No more items with ID {ItemID} available for placement.");
            }
        }
    }


