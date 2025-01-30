using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHUDManager : MonoBehaviour
{
    public Transform PauseMenu;

    [SerializeField]
    DialogueSystemController dialogueController;

    private GameManagement gameManagement;

    private void Awake()
    {
        gameManagement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.gameObject.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0.0f;
        }
    }

    public void ShowGoalDialogue()
    {
        dialogueController.ShowDialogue();
        Cursor.visible = true;
    }

    public void LoadPlacementScene()
    {
        gameManagement.IncreaseLevel();
        gameManagement.LoadPlacementScene();
    }

}
