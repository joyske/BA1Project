using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]
    Inventory inventorySO;

    [SerializeField]
    InventorySceneData inventorySceneData;

    private Dictionary<int, int> placedItemCounts = new Dictionary<int, int>();  // { ItemID, PlacedCount }

    int currentSceneID = 0;

    GameManagement gameManagement;

    [SerializeField]
    List<ItemSlotUI> inventorySlots;

    [SerializeField]
    MenuScript menuScript;


    private void Awake()
    {
        gameManagement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        currentSceneID = gameManagement.currentLevelIndex;
    }

    private void Start()
    {
        InitItemCounts();
        InitUI();

    }

    private void InitUI()
    {

        int slotIndex = 0;
        foreach (var item in placedItemCounts)
        {
           
            if (slotIndex >= inventorySlots.Count)
            {
                Debug.LogWarning("Not enough slots to display all items.");
                break;
            }

            Sprite itemSprite = GetSpriteByID(item.Key);
            inventorySlots[slotIndex].SetData(item.Key, itemSprite, item.Value);
            slotIndex++;

        }

        for (int i = slotIndex; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].DisableButton();
        }
    }


    public Sprite GetSpriteByID(int itemID)
    {

        foreach (var data in inventorySO.objectsData) 
        {
            if (data.ID == itemID)
            {
                return data.sprite; 
            }
        }
        Debug.LogWarning($"Sprite with ID {itemID} not found!");
        return null; 
    }



    /// <summary>
    ///  Initalize Dictionary with Items and the amount to place
    /// </summary>
    private void InitItemCounts()
    {
        InventoryCountData sceneData = inventorySceneData.SceneData.Find(scene => scene.SceneID == currentSceneID);

        if (sceneData != null)
        {
            foreach (var countData in sceneData.CountData)
            {
                placedItemCounts[countData.ItemID] = countData.TotalItemCount;
            }
        }

        //foreach (var obj in placedItemCounts)
        //{
        //    Debug.Log($"kid {obj.Key}");
        //    Debug.Log($"quantity {obj.Value}");

        //}

    }

    public void IncrementPlacedItem(int itemID)
    {
        placedItemCounts[itemID]++;
        inventorySlots.FirstOrDefault(s => s.GetItemID() == itemID).UpdateQuantity(placedItemCounts[itemID]);
        menuScript.UpdateStart(CanStartGame());
    }

    public void DecreasePlacedItem(int itemID)
    {
        placedItemCounts[itemID]--;
        ItemSlotUI itemSlot = inventorySlots.FirstOrDefault(s => s.GetItemID() == itemID);
        itemSlot.UpdateQuantity(placedItemCounts[itemID]);
        if (placedItemCounts[itemID] == 0)
        {
            itemSlot.enabled = false;
        }
        menuScript.UpdateStart(CanStartGame());
    }

    public bool CanPlaceItem(int itemID)
    {
        if (placedItemCounts[itemID] == 0)
        {
            return false;

        }
        return true;
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


    public void ResetPlacementData()
    {
        placedItemCounts.Clear();
    }

    public bool CanStartGame()
    {
        return placedItemCounts.Values.All(value => value == 0);
    }
}