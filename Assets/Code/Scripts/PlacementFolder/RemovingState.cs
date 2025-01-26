using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IPlacementState
{
    // TODO create seperate class since same fields in states?
    int gameObjectindex = -1;   
    Grid grid;
    PreviewSystem previewSystem;
    GridData gridData;
    CargoPlacement cargoPlacement;
    InventoryUIManager inventoryManager;

    public RemovingState(Grid grid, PreviewSystem previewSystem,  GridData gridData, CargoPlacement cargoPlacement, InventoryUIManager inventoryManager)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.gridData = gridData;
        this.cargoPlacement = cargoPlacement;
        this.inventoryManager = inventoryManager;


        previewSystem.StartShowingRemovePreview();
        this.inventoryManager = inventoryManager;
    }

    public void EndState()
    {
        // TODO object unhighlighten
    }

    public void OnAction(Vector3Int gridPos)
    {

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

        GridData selectedData = null;
        if (gridData.CanPlaceObjectAt(gridPos, Vector3Int.one) == false) 
        { 
            selectedData = gridData;
        }


        if (selectedData == null) 
        {
            //sound
        } else
        {
            gameObjectindex = selectedData.GetIndex(gridPos);
            if (gameObjectindex == -1)
                return;
            int itemID = selectedData.GetObjectID(gridPos);
            selectedData.RemoveObjectAt(gridPos);
            cargoPlacement.RemoveObjectAt(gameObjectindex);
            inventoryManager.IncrementPlacedItem(itemID);
        }
        Vector3 cellPos = grid.CellToWorld(gridPos);
        previewSystem.UpdatePosition(cellPos, CheckIfSelectionIsValid(gridPos));

    }


    private bool CheckIfSelectionIsValid(Vector3Int gridPos)
    {
        return !gridData.CanPlaceObjectAt(gridPos, Vector3Int.one);
    }

    public void UpdateState(Vector3Int gridPos)
    {
        bool validity = CheckIfSelectionIsValid(gridPos);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPos), validity);
    }
}
