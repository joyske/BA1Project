using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IPlacementState
{
    int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    Inventory inventory;
    GridData gridData;
    CargoManager cargoManager;
    InventoryUIManager inventoryManager;
    List<AudioClip> placementSounds;
    AudioSource audioSource;

    public PlacementState(int iD, Grid grid, PreviewSystem previewSystem, Inventory inventory, GridData gridData, CargoManager cargoManager, InventoryUIManager inventoryManager, List<AudioClip> placementSounds, AudioSource audioSource)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.inventory = inventory;
        this.gridData = gridData;
        this.cargoManager = cargoManager;
        this.inventoryManager = inventoryManager;
        this.placementSounds = placementSounds;
        this.audioSource = audioSource;

        selectedObjectIndex = inventory.objectsData.FindIndex(data => data.ID == ID);  
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"NO ID FOUND {ID}");
        }

        if (selectedObjectIndex > -1)
        {

            previewSystem.StartShowingPlacementPreview(inventory.objectsData[selectedObjectIndex].Prefab);
        }
        else
        {
            throw new System.Exception($"No object with ID {ID}");
        }

    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    /// <summary>
    /// Places an item if valid, updates grid data and inventory.
    /// </summary>
    /// <param name="gridPos">Target grid position.</param>
    public void OnAction(Vector3Int gridPos)
    {
        if (inventoryManager.CheckIfItemsLeft(ID))
        {
            bool placementValid = CheckPlacementValidity(gridPos, selectedObjectIndex);
            if (!placementValid) return;

            int index = cargoManager.Placement(inventory.objectsData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPos));

            gridData.AddObjectAt(gridPos, inventory.objectsData[selectedObjectIndex].Size, inventory.objectsData[selectedObjectIndex].ID, index);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPos), false);
            inventoryManager.DecreasePlacedItem(inventory.objectsData[selectedObjectIndex].ID);
            if (!inventoryManager.CheckIfItemsLeft(ID))
            {
                previewSystem.StopShowingPreview();
            }
            audioSource.PlayOneShot(placementSounds[Random.Range(0, placementSounds.Count)]);
        } 
        else
        {
            previewSystem.StopShowingPreview();
        }
    }


    /// <summary>
    /// Check if object can be placed on cell
    /// </summary>
    /// <param name="gridPos"></param>
    /// <param name="selectedObjectIndex"></param>
    /// <returns></returns>
    private bool CheckPlacementValidity(Vector3Int gridPos, int selectedObjectIndex)
    {
        return gridData.CanPlaceObjectAt(gridPos, inventory.objectsData[selectedObjectIndex].Size);
    }


    public void UpdateState(Vector3Int gridPos)
    {
        bool placementValid = CheckPlacementValidity(gridPos, selectedObjectIndex);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPos), placementValid);
    }
}
