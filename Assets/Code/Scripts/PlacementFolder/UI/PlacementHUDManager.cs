using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlacementHUDManager : MonoBehaviour
{


    [SerializeField] PlacementManager placementManager;
    private GridData gridData;


    [SerializeField] Toggle simulateToggle;
    [SerializeField] RectTransform simulateSwitch;
    [SerializeField] Button startButton;
    [SerializeField] Button deleteButton;
    private GameManagement gameManagement;



    public void Awake()
    {
        gameManagement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gridData = GameObject.FindWithTag("CargoData").GetComponent<GridData>();
    }

    public static GameObject GetEventClickedButton()
    {
        EventSystem currentEvent = EventSystem.current;
        return currentEvent.currentSelectedGameObject;
    }

    public void LoadLevel()
    {
        gameManagement.LoadLevelScene();
        Time.timeScale = 1.0f;
    }

    public void ToggleSimulation()
    {
        if (simulateToggle.isOn) { StartSimulation(); return; }
        ResetSimulation();
    }


    public void StartToggle(Toggle toggle, RectTransform switchTransform)
    {
        switchTransform.localPosition = new Vector3(10f, 0f, 0f);
        //toggle.transform.GetChild(0).GetComponent<Image>().color = Color.green;
        toggle.isOn = true;
    }

    public void EndToggle(Toggle toggle, RectTransform switchTransform)
    {
        switchTransform.localPosition = new Vector3(-10f, 0f, 0f);
        //toggle.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        toggle.isOn = false;
    }

    public void StartSimulation()
    {
        StartToggle(simulateToggle, simulateSwitch);
        transform.GetChild(0).transform.gameObject.SetActive(false);
        transform.GetChild(1).GetComponent<Button>().interactable = false;
        transform.GetChild(2).GetComponent<Button>().interactable = false;
        placementManager.StartSimulation();
    }

    public void ResetSimulation()
    {
        transform.GetChild(0).transform.gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<Button>().interactable = true;
        transform.GetChild(2).GetComponent<Button>().interactable = true;
        EndToggle(simulateToggle, simulateSwitch);
        placementManager.ResetObjects();
    }

    public void LoadStackingSystem()
    {
        Destroy(gridData.gameObject);
        gameManagement.LoadPlacementScene();
    }

    public void Delete()
    {
        placementManager.StartRemoving();

    }

    public void InDeleteMode(bool inDelete)
    {
        Color color = inDelete ? Color.red : Color.white;
        deleteButton.GetComponent<Image>().color = color;
    }

    public void UpdateStart(bool CanStart) => startButton.interactable = CanStart;
}
