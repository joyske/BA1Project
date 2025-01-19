using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] PlacementSystem placementSystem;
    private GridData gridData;

    public void Awake()
    {
        
        gridData = GameObject.FindWithTag("CargoData").GetComponent<GridData>();
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gridData.placedObjects.Clear();
        }*/

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            this.gameObject.SetActive(false);
        }
       
    }
    public void LoadLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);       
    }

    public void StartSimulation()
    {
        placementSystem.StartSimulation();
    }

    public void ResetSimulation()
    {
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
