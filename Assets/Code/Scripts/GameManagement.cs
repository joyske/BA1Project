using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
   

    public int currentLevelIndex = 0;

    public string[] levelScenes = { "FirstLevel", "SecondLevel", "ThirdLevel", "FourthLevel"};
    //public  string[] placementScenes = { "PlacementScene1", "PlacementScene2", "PlacementScene3" };
    GridData gridData;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!(SceneManager.GetActiveScene().buildIndex == 0))
        {
            GameObject cargo = GameObject.Find("CargoData");
            gridData = cargo.GetComponent<GridData>();
        }

    }


    public void LoadLevelScene()
    {
        SceneManager.LoadScene(levelScenes[currentLevelIndex]);
    }

    public void LoadPlacementScene()
    {
        Time.timeScale = 1.0f;
        if (currentLevelIndex == SceneManager.sceneCountInBuildSettings-2)
        {
            LoadTitleScreen();
        }
        else
        {
            SceneManager.LoadScene("Placement");
        }


    }

    public void IncreaseLevel()
    {
        DestroyCargo();
        currentLevelIndex++;
    }

    public void DestroyCargo()
    {
        GameObject cargo = GameObject.Find("CargoData");
        gridData = cargo.GetComponent<GridData>();
        Destroy(gridData.gameObject);
    }

    public void LoadTitleScreen()
    {
        DestroyCargo();
        Destroy(gameObject);
        currentLevelIndex = 0;
        SceneManager.LoadScene(0);
    }

}
