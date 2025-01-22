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

    public void Awake()
    {
        
        gridData = GameObject.FindWithTag("CargoData").GetComponent<GridData>();
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gridData.placedObjects.Clear();
        }*/
       
    }
    public void LoadLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);       
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
            StartToggle(deleteToggle, deleteSwitch); 
            return; 
        }
        EndToggle(deleteToggle, deleteSwitch);
        placementSystem.StartPlacement(placementSystem.lastUsedIndex); 
        
    }

    public void StartToggle(Toggle toggle, RectTransform switchTransform)
    {
        switchTransform.localPosition = new Vector3(15f, 0f, 0f);
        toggle.transform.GetChild(0).GetComponent<Image>().color = Color.green;
    }

    public void EndToggle(Toggle toggle, RectTransform switchTransform)
    {
        switchTransform.localPosition = new Vector3(-15f, 0f, 0f);
        toggle.transform.GetChild(0).GetComponent<Image>().color = Color.red;
    }

    public void StartSimulation()
    {
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
        SceneManager.LoadScene(0);
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


}
