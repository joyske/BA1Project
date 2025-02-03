using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadCargo : MonoBehaviour
{
    Grid grid;
    GridData gridData;

    [SerializeField]
    Inventory inventory;

    [SerializeField]
    GameObject boat;

    [SerializeField]
    float delayTime = 2.0f;

    [SerializeField] float yRotation = 0f;

    private GameObject newObject;

    PlayerHUD hud;

    [SerializeField]
    Image loadingScreen;

    private void Awake()
    {
        Vector3 targetPos = new Vector3(0, 0, 0);
        newObject = Instantiate(boat, targetPos, Quaternion.identity);
        hud = GameObject.FindWithTag("HUD").GetComponent<PlayerHUD>();
        hud.InitHUD();
        StartCoroutine(InitializeBoatAndCargo());
    }   

    /// <summary>
    /// Instantiates the boat, waits for it to settle, and then places the cargo
    /// </summary>
    private IEnumerator InitializeBoatAndCargo()
    {
        Vector3 targetPos = new Vector3(0, 0, 0);
        newObject.transform.eulerAngles = new Vector3(newObject.transform.eulerAngles.x, yRotation, newObject.transform.eulerAngles.z);
        grid = newObject.GetComponentInChildren<Grid>();
        newObject.GetComponent<ShipMovement>().enabled = false;

        yield return new WaitForSeconds(delayTime);

        newObject.GetComponent<Rigidbody>().isKinematic = true;
        GameObject cargo = GameObject.Find("CargoData");
        gridData = cargo.GetComponent<GridData>();
        PlaceSavedObjects();
        GameObject.Find("GroundGrid").SetActive(false);
        newObject.GetComponent<Rigidbody>().isKinematic = false;
        newObject.GetComponent<ShipMovement>().enabled = true;
        loadingScreen.enabled = false;
        

    }

    /// <summary>
    /// Places all objects saved in GridData on the boat
    /// </summary>
    public void PlaceSavedObjects()
    {

        foreach (var entry in gridData.placedObjects)
        {
            Vector3Int position = entry.Key; // Grid position
            var data = entry.Value;

            // Find the prefab based on the ID in the inventory
            var prefabData = inventory.objectsData.Find(obj => obj.ID == data.ID);

            // Instantiate and position the object
            GameObject obj = Instantiate(prefabData.Prefab, grid.CellToWorld(position), Quaternion.identity);
            PhysicsToggle.EnablePhysics(obj);

        }
    }

   
}