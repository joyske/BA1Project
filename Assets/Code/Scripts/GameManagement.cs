using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
   

    public int currentLevelIndex = 0;

    public string[] levelScenes = { "FirstLevel", "SecondLevel"};
    //public  string[] placementScenes = { "PlacementScene1", "PlacementScene2", "PlacementScene3" };


    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void LoadLevelScene()
    {
        SceneManager.LoadScene(levelScenes[currentLevelIndex]);
        //currentLevelIndex++;
    }

    public void LoadPlacementScene()
    {
        SceneManager.LoadScene("Placement");
    }



}
