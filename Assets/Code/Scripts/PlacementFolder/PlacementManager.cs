using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlacementManager : MonoBehaviour
{

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

    [SerializeField]
    CargoManager cargoManager;

    [SerializeField]
    IPlacementState placementState;

    [SerializeField]
    InventoryUIManager inventoryManager;

    [SerializeField]
    PlacementHUDManager hudManager;

    public int lastUsedIndex;

    bool isSimulating, isRemoving;

    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> placementSounds;


    [SerializeField]
    private AudioClip removalSound;

    private void Start()
    {
        isSimulating = false;
        isRemoving = false; 
        StopPlacement();
        Cursor.visible = true;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Starts placement state for an item with the given ID.
    /// </summary>
    public void StartPlacement(int ID)
    {
        if (!isSimulating && !isRemoving && inventoryManager.CheckIfItemsLeft(ID))
        {
            hudManager.InDeleteMode(isRemoving);
            inventoryManager.SetButtonSelection(lastUsedIndex, false);
            inventoryManager.SetButtonSelection(ID, true);

            lastUsedIndex = ID;
            StopPlacement();
            gridVisualization.SetActive(true);
            placementState = new PlacementState(ID, grid, previewSystem, inventory, gridData, cargoManager, inventoryManager, placementSounds, audioSource);
            inputManager.OnClicked += PlaceItem;
            inputManager.OnExit += StopPlacement;
        }
    }

    /// <summary>
    /// Starts removing state.
    /// </summary>
    public void StartRemoving()
    {
        if (!isSimulating)
        {
            inventoryManager.SetButtonSelection(lastUsedIndex, false);
            isRemoving = true;
            hudManager.InDeleteMode(isRemoving);
            StopPlacement();
            gridVisualization.SetActive(true);
            placementState = new RemovingState(grid, previewSystem, gridData, cargoManager, inventoryManager, removalSound, audioSource);
            inputManager.OnClicked += PlaceItem;
            inputManager.OnExit += StopPlacement;
            isRemoving = false;
        }
    }


    /// <summary>
    /// Places an item at the selected grid position
    /// </summary>
    private void PlaceItem()
    {
        if (inputManager.IsPointOverUI())
        {
            return;
        }
        Vector3 mousePos = inputManager.GetSelectedMousePos();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

       placementState.OnAction(gridPos);

    }

    /// <summary>
    /// Stops placement/removal, disables grid visualization, and clears state.
    /// </summary>
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
        if (placementState == null) { return; }

        // Get mouse postion and convert to grid position
        Vector3 mousePos = inputManager.GetSelectedMousePos();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        // check if mouse moved
        if (lastDetectedPos != gridPos)
        {
            placementState.UpdateState(gridPos);
            lastDetectedPos = gridPos;
        }
    }

    /// <summary>
    /// Enables physics and starts simulation
    /// </summary>
    public void StartSimulation()
    {
        inventoryManager.SetButtonSelection(lastUsedIndex, false);
        hudManager.InDeleteMode(isRemoving);
        isSimulating = true;
        StopPlacement();
        cargoManager.SimulatePhysics();
    }

    ///// <summary>
    ///// Destroys all objects and resets the positions from gridata 
    ///// </summary>
    public void ResetObjects()
    {
        inventoryManager.SetButtonSelection(lastUsedIndex, false);
        isSimulating = false;
        StopPlacement();
        cargoManager.DestroyAllCargo();
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

            // Instantiate and position the object
            GameObject newObject = Instantiate(prefabData.Prefab, grid.CellToWorld(position), Quaternion.identity);
            cargoManager.AddItemToList(newObject, data.PlacedObjectIndex);
        }
    }


}


