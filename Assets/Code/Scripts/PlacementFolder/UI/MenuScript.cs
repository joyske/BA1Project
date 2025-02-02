using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private GridData gridData;
    private GameManagement gameManagement;
    private GameObject startButtons;
    public GameObject dropdownPanel;
    private bool toggleRestart = false;

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
        Debug.Log("here");
        gameManagement.LoadLevelScene();
        Time.timeScale = 1.0f;
    }



    public void ToggleRestart()
    {
        RectTransform restartButtonTrigger = GetComponentInChildren<EventTrigger>().transform.GetComponent<RectTransform>();

        startButtons = transform.GetChild(transform.childCount - 3).gameObject;
        toggleRestart = !toggleRestart;
        startButtons.SetActive(toggleRestart);
        float offset;
        if (!toggleRestart) { offset = -297f; } else { offset = -170f; }
        restartButtonTrigger.offsetMax = new Vector2(offset, restartButtonTrigger.offsetMax.y);

    }

    public void ShowRestartButtons()
    {
        dropdownPanel.SetActive(true);
    }

    public void HideRestartButtons()
    {
        dropdownPanel.SetActive(false);
    }

    public void LoadStackingSystem()
    {
        Debug.Log("here2");
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
        gameManagement.LoadTitleScreen();
    }

    public void ExitGame()
    {
        Application.Quit();
    }





}
