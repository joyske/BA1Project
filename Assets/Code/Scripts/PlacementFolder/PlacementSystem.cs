using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    Inventory inventory;

    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    GridData GridData;

    private List<GameObject> placedGameObjects = new();

    [SerializeField]
    private PreviewSystem previewSystem;

    private Vector3Int lastDetectedPos = Vector3Int.zero;

    [SerializeField]
    Transform boatTransform;



    private void Start()
    {
        Vector3 gridPosition = grid.transform.position;
        gridPosition.y = boatTransform.position.y;  
        grid.transform.position = gridPosition;
        StopPlacement();
       //GridData = new(); // item == object TODO name entscheiden 
    }


    /// <summary>
    /// Start Placemnt of Item with given ID 
    /// </summary>
    /// <param name="ID"></param>
    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = inventory.objectsData.FindIndex(data => data.ID == ID);  // Get item with id from inventory
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"NO ID FOUND {ID}");
        }
        //Renderer renderer = gridVisualization.GetComponent<Renderer>();
        //renderer.enabled = true;
        gridVisualization.SetActive(true);
        previewSystem.StartShowingPlacementPreview(inventory.objectsData[selectedObjectIndex].Prefab);
        inputManager.OnClicked += PlaceItem;
        inputManager.OnExit += StopPlacement;
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
        Debug.Log(mousePos);
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        //gridPos.y = Mathf.RoundToInt(boatTransform.position.y);

        bool placementValid = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if (!placementValid) return;

        GameObject newObject = Instantiate(inventory.objectsData[selectedObjectIndex].Prefab);


        //Vector3 placementPosition = grid.CellToWorld(gridPos);
        //placementPosition.y = boatTransform.position.y;
        //newObject.transform.position = placementPosition;

        newObject.transform.position = grid.CellToWorld(gridPos);


        placedGameObjects.Add(newObject);
        Debug.Log(gridPos);
        GridData.AddObjectAt(gridPos, inventory.objectsData[selectedObjectIndex].Size, inventory.objectsData[selectedObjectIndex].ID, placedGameObjects.Count -1);
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
        return GridData.CanPlaceObjectAt(gridPos, inventory.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex--;
        //Renderer renderer = gridVisualization.GetComponent<Renderer>();
        //renderer.enabled = false;
        gridVisualization.SetActive(false);
        previewSystem.StopShowingPreview();
        inputManager.OnClicked -= PlaceItem;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPos = Vector3Int.zero;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StopPlacement();
            Debug.Log(placedGameObjects.Count);
            foreach (GameObject obj in placedGameObjects)
            {
                // enable physics simulation
               Rigidbody rb=  obj.GetComponentInChildren<Rigidbody>();
               rb.isKinematic = false;
                // toggle colliders for physics simulation TODO cylinderC
                SphereCollider sphereCollider = obj.GetComponentInChildren<SphereCollider>();
                if (sphereCollider != null)
                {
                    BoxCollider boxCollider = obj.GetComponentInChildren<BoxCollider>();
                    if (boxCollider != null)
                    {
                        boxCollider.enabled = false;
                    }
                    sphereCollider.enabled = true;
                
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetObjects();
        }


        if (selectedObjectIndex < 0) { return; }
        // Get mouse postion and convert to grid position
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        if (lastDetectedPos != gridPos)
        {
            bool placementValid = CheckPlacementValidity(gridPos, selectedObjectIndex);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPos), placementValid);
            lastDetectedPos = gridPos;
        }
     
        //mouseIndicator.transform.position = mousePos;
    }


    /// <summary>
    /// Destroys all objects and resets the positions from gridata 
    /// </summary>
    public void ResetObjects()
    {
        foreach (GameObject obj in placedGameObjects)
        {
            Destroy(obj);
        }
        StopPlacement();
        placedGameObjects.Clear();
        PlaceSavedObjects();
    }


    /// <summary>
    /// Places all objects saved in Griddata
    /// </summary>
    public void PlaceSavedObjects()
    {
        foreach (var entry in GridData.placedObjects)
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
            placedGameObjects.Add(newObject);
        }
    }


}

