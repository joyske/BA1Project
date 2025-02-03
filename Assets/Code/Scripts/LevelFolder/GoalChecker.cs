using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{

    private GameManagement gameManagement;
    private GridData gridData;
    private LevelHUDManager levelHUDManager;
    bool dialogeShown = false;

    public void Awake()
    {
        gameManagement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gridData = GameObject.FindWithTag("CargoData").GetComponent<GridData>();
       
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            levelHUDManager = GameObject.FindWithTag("SceneManager").GetComponent<LevelHUDManager>();
            if (!dialogeShown )
            {
                PlayerHUD playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<PlayerHUD>();
                playerHUD.InGoal();
                levelHUDManager.ShowGoalDialogue();     
                dialogeShown = true;
                Time.timeScale = 0.0f;
            }

  
            //Destroy(gridData.gameObject);
        }
    }
}
