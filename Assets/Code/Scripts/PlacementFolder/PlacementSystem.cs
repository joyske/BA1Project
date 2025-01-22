using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlacementSystem : MonoBehaviour
{

    public int lastUsedIndex;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    Inventory inventory;


    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    GridData gridData;

    [SerializeField]
    private PreviewSystem previewSystem;

    private Vector3Int lastDetectedPos = Vector3Int.zero;

    [SerializeField]
    Transform boatTransform;

    bool isSimulating, isRemoving;

    [SerializeField]
    CargoPlacement cargoPlacement;

    [SerializeField]
    IPlacementState placementState;

    [SerializeField]
    InventorySceneData inventorySceneData;

    private void Start()
    {
        isSimulating = false;
        isRemoving = false; 
        Vector3 gridPosition = grid.transform.position;
        //gridPosition.y = boatTransform.position.y;  
        grid.transform.position = gridPosition;
        StopPlacement();
        //GridData = new(); // item == object TODO name entscheiden 

        Cursor.visible = true;
    }


    /// <summary>
    /// Start Placemnt of Item with given ID 
    /// </summary>
    /// <param name="ID"></param>
    public void StartPlacement(int ID)
    {
        if (!isSimulating && !isRemoving)
        {
            lastUsedIndex = ID;
            StopPlacement();
            gridVisualization.SetActive(true);
            placementState = new PlacementState(ID, grid, previewSystem, inventory, gridData, cargoPlacement);
            inputManager.OnClicked += PlaceItem;
            inputManager.OnExit += StopPlacement;
        }
    }


    public void StartRemoving()
    {   if (!isSimulating)
        {
            isRemoving = true;
            StopPlacement();
            gridVisualization.SetActive(true);
            placementState = new RemovingState(grid, previewSystem, gridData, cargoPlacement);
            inputManager.OnClicked += PlaceItem;
            inputManager.OnExit += StopPlacement;
            isRemoving = false;
        }
    }


    /// <summary>
    /// Place item on grid 
    /// </summary>
    private void PlaceItem()
    {
        if (inputManager.IsPointOverUI())
        {
            return;
        }
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

       placementState.OnAction(gridPos);

    }

    ///// <summary>
    ///// Check if object can be placed on cell
    ///// </summary>
    ///// <param name="gridPos"></param>
    ///// <param name="selectedObjectIndex"></param>
    ///// <returns></returns>
    //private bool CheckPlacementValidity(Vector3Int gridPos, int selectedObjectIndex)
    //{
    //    return gridData.CanPlaceObjectAt(gridPos, inventory.objectsData[selectedObjectIndex].Size);
    //}


    private void StopPlacement()
    {
        if (placementState == null) return;
        gridVisualization.SetActive(false);
        placementState.EndState();
        inputManager.OnClicked -= PlaceItem;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPos = Vector3Int.zero;
        placementState = null;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSimulation();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetObjects();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRemoving();
        }


        if (placementState == null) { return; }
        // Get mouse postion and convert to grid position
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if (lastDetectedPos != gridPos)
        {
            placementState.UpdateState(gridPos);
            lastDetectedPos = gridPos;
        }
     
        //mouseIndicator.transform.position = mousePos;
    }




    //TODO Simulationsmethoden als State ?? Definitv PlaceSavedObjects

    /// <summary>
    /// Enables physics and starts simulation
    /// </summary>
    public void StartSimulation()
    {
        isSimulating = true;
        StopPlacement();
        cargoPlacement.EnablePhysics();  
    }

    ///// <summary>
    ///// Destroys all objects and resets the positions from gridata 
    ///// </summary>
    public void ResetObjects()
    {
        isSimulating = false;
        StopPlacement();
        cargoPlacement.DestroyAllCargo();
        PlaceSavedObjects();
    }


    ///// <summary>
    ///// Places all objects saved in Griddata
    ///// </summary>
    public void PlaceSavedObjects()
    {
        foreach (var entry in gridData.placedObjects)
        {
            Vector3Int position = entry.Key; // Grid position
            var data = entry.Value;

            // Find the prefab based on the ID in the inventory
            var prefabData = inventory.objectsData.Find(obj => obj.ID == data.ID);
            if (prefabData == null)
            {
                Debug.LogWarning($"No prefab found for ID {data.ID}");
                continue;
            }

            // Instantiate and position the object
            GameObject newObject = Instantiate(prefabData.Prefab, grid.CellToWorld(position), Quaternion.identity);
            cargoPlacement.AddCargoToList( newObject );
        }
    }


}

