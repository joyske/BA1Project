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
    CargoPlacement cargoPlacement;

    public PlacementState(int iD, Grid grid, PreviewSystem previewSystem, Inventory inventory, GridData gridData, CargoPlacement cargoPlacement)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.inventory = inventory;
        this.gridData = gridData;
        this.cargoPlacement = cargoPlacement;


        selectedObjectIndex = inventory.objectsData.FindIndex(data => data.ID == ID);  // Get item with id from inventory
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


    public void OnAction(Vector3Int gridPos)
    {
        //Debug.Log(gridPos);
        bool placementValid = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if (!placementValid) return;

        int index = cargoPlacement.Placement(inventory.objectsData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPos));

        gridData.AddObjectAt(gridPos, inventory.objectsData[selectedObjectIndex].Size, inventory.objectsData[selectedObjectIndex].ID, index);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPos), false);
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
