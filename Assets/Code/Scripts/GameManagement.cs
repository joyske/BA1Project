using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; } // Singleton instance

    public int currentLevelIndex = 0;
    public string[] levelScenes = { "FirstLevel", "SecondLevel", "ThirdLevel", "FourthLevel" };
    GridData gridData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }

    void Start()
    {
        if (!(SceneManager.GetActiveScene().buildIndex == 0))
        {
            GameObject cargo = GameObject.Find("CargoData");
            if (cargo != null)
            {
                gridData = cargo.GetComponent<GridData>();
            }
        }
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene(levelScenes[currentLevelIndex]);
    }

    public void LoadPlacementScene()
    {
        Time.timeScale = 1.0f;
        if (currentLevelIndex == SceneManager.sceneCountInBuildSettings - 2)
        {
            LoadTitleScreen();
        }
        else
        {
            SceneManager.LoadScene("Placement");
        }
    }

    /// <summary>
    /// Increase Level when switching to next Level
    /// </summary>
    public void IncreaseLevel()
    {
        DestroyCargo();
        currentLevelIndex++;
    }

    public void DestroyCargo()
    {
        GameObject cargo = GameObject.Find("CargoData");
        if (cargo != null)
        {
            gridData = cargo.GetComponent<GridData>();
            Destroy(gridData.gameObject);
        }
    }

    public void LoadTitleScreen()
    {
        DestroyCargo();
        Destroy(gameObject);
        Instance = null; // Reset instance so a new one can be created
        currentLevelIndex = 0;
        SceneManager.LoadScene(0);
    }
}
