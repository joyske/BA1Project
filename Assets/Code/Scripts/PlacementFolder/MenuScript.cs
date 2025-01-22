using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] PlacementSystem placementSystem;
    private GridData gridData;

    [SerializeField] Toggle deleteToggle;
    [SerializeField] RectTransform deleteSwitch;
    [SerializeField] Toggle simulateToggle;
    [SerializeField] RectTransform simulateSwitch;
    private GameManagement gameManagement;

    public void Awake()
    {
        gameManagement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gridData = GameObject.FindWithTag("CargoData").GetComponent<GridData>();
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gridData.placedObjects.Clear();
        }*/

    }
    public void LoadLevel()
    {
        //SceneManager.LoadScene(1);
        gameManagement.LoadLevelScene();
        Time.timeScale = 1.0f;
    }

    public void ToggleSimulation()
    {
        if (simulateToggle.isOn) { StartSimulation(); return; }
        ResetSimulation();
    }

    public void ToggleDelete()
    {
        if (deleteToggle.isOn) 
        { 
            placementSystem.StartRemoving();
            EndToggle(simulateToggle, simulateSwitch);
            StartToggle(deleteToggle, deleteSwitch); 
            return; 
        }
        EndToggle(deleteToggle, deleteSwitch);
        placementSystem.StartPlacement(placementSystem.lastUsedIndex); 
        
    }

    public void StartToggle(Toggle toggle, RectTransform switchTransform)
    {
        switchTransform.localPosition = new Vector3(10f, 0f, 0f);
        toggle.transform.GetChild(0).GetComponent<Image>().color = Color.green;
        toggle.isOn = true;
    }

    public void EndToggle(Toggle toggle, RectTransform switchTransform)
    {
        switchTransform.localPosition = new Vector3(-10f, 0f, 0f);
        toggle.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        toggle.isOn = false;
    }

    public void StartSimulation()
    {
        EndToggle(deleteToggle, deleteSwitch);
        StartToggle(simulateToggle, simulateSwitch);
        placementSystem.StartSimulation();
    }

    public void ResetSimulation()
    {
        EndToggle(simulateToggle, simulateSwitch);
        placementSystem.ResetObjects();
    }

    public void LoadStackingSystem()
    {
        Time.timeScale = 1.0f;
        Destroy(gridData.gameObject);
        gameManagement.LoadPlacementScene();
        //gameManagement.currentLevelIndex--;
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        //SceneManager.LoadScene(?);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Delete()
    {
        placementSystem.StartRemoving();
    }


}
