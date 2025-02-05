using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IPlacementState
{
    int gameObjectindex = -1;   
    Grid grid;
    PreviewSystem previewSystem;
    GridData gridData;
    CargoManager cargoManager;
    InventoryUIManager inventoryManager;
    AudioClip removalSound;
    AudioSource audioSource;

    public RemovingState(Grid grid, PreviewSystem previewSystem,  GridData gridData, CargoManager cargoManager, InventoryUIManager inventoryManager, AudioClip removalSound, AudioSource audioSource)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.gridData = gridData;
        this.cargoManager = cargoManager;
        this.inventoryManager = inventoryManager;
        this.removalSound = removalSound;
        this.audioSource = audioSource;
        this.inventoryManager = inventoryManager;
    }

    public void EndState()
    {
    }


    /// <summary>
    /// Gets object on selected position, delete it from data and scene
    /// </summary>
    /// <param name="gridPos"></param>
    public void OnAction(Vector3Int gridPos)
    {
        // Get objetct on mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                Vector3 worldPosition =  hit.collider.gameObject.transform.position; 
                gridPos = grid.WorldToCell(worldPosition);  
            }

        }

        // check if pos is valid 
        GridData selectedData = null;
        if (gridData.CanPlaceObjectAt(gridPos, Vector3Int.one) == false) 
        { 
            selectedData = gridData;
        }

        if (selectedData != null) 
        {
            // delete from data and scene and update ui
            gameObjectindex = selectedData.GetIndex(gridPos);
            if (gameObjectindex == -1)
                return;
            int itemID = selectedData.GetObjectID(gridPos);
            selectedData.RemoveObjectAt(gridPos);
            cargoManager.RemoveObjectAt(gameObjectindex);
            inventoryManager.IncrementPlacedItem(itemID);
            audioSource.PlayOneShot(removalSound);
        }

        Vector3 cellPos = grid.CellToWorld(gridPos);
    

    }


    private bool CheckIfSelectionIsValid(Vector3Int gridPos)
    {
        return !gridData.CanPlaceObjectAt(gridPos, Vector3Int.one);
    }

    public void UpdateState(Vector3Int gridPos)
    {
        bool validity = CheckIfSelectionIsValid(gridPos);
    }
}
