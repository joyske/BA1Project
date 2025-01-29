using System.Collections;
using System.Collections.Generic;
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
        GameObject cargo = GameObject.Find("CargoData");
        gridData = cargo.GetComponent<GridData>();
    }


    public void LoadLevelScene()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene(levelScenes[currentLevelIndex]);
        //currentLevelIndex++;
    }

    public void LoadPlacementScene()
    {
        SceneManager.LoadScene("Placement");
    }

    public void IncreaseLevel()
    {
        GameObject cargo = GameObject.Find("CargoData");
        gridData = cargo.GetComponent<GridData>();
        Destroy(gridData.gameObject);
        currentLevelIndex++;
    }

}
