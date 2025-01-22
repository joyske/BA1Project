using System.Collections.Generic;
using UnityEngine;

public class RuntimeInventoryTracker : MonoBehaviour
{

    InventorySceneData inventorySceneData;
    private Dictionary<int, int> placedItemCounts = new Dictionary<int, int>();  // { ItemID, PlacedCount }
    int sceneID = 0;    

    // TODO hier data und szene übergeben
    public RuntimeInventoryTracker()
    {
        inventorySceneData = new InventorySceneData();
    }

    public void IncrementPlacedItem(int itemID)
    {
        if (!placedItemCounts.ContainsKey(itemID))
        {
            placedItemCounts[itemID] = 0;  
        }

        placedItemCounts[itemID]++;
    }

    // Get the placed count for an item
    public int GetPlacedItemCount(int itemID)
    {
        if (placedItemCounts.ContainsKey(itemID))
        {
            return placedItemCounts[itemID];
        }
        return 0;
    }

    public int GetTotalItemCount(int sceneID, int itemID)
    {
        var sceneData = inventorySceneData.SceneData.Find(data => data.SceneID == sceneID);
        var countData = sceneData?.CountData.Find(data => data.ItemID == itemID);
        return countData?.TotalItemCount ?? 0;
    }

    // Reset placed items count when the scene reloads or restarts
    public void ResetPlacementData()
    {
        placedItemCounts.Clear();
    }

    public int GetRemainingCount(int itemID)
    {
       int currentCount = GetPlacedItemCount(itemID);
       int totalCount = GetTotalItemCount(sceneID, itemID);
       return  totalCount - currentCount ;
       
    }
}