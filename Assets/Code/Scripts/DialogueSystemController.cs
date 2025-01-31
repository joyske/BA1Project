using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueSystemController : MonoBehaviour
{
    [SerializeField]
    NPCData npcData;

    NPCDialogueData npcDialogueData;

    GameManagement gameManagement;

    string dialogueText;

    int sceneID;

    PlayerHUD playerHUD;

    [SerializeField]
    DialogueUI dialogueUI;

    [SerializeField]
    Image[] stars;


    void InitDialogues()
    {
        gameManagement = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagement>();

        sceneID = gameManagement.currentLevelIndex;
        npcDialogueData = npcData.NPCDialogueData.Find(data => data.SceneID == sceneID);


        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            dialogueText = GetMissionDialogue();
        }
        else
        {
            playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<PlayerHUD>();
            //playerHUD.InGoal();
            int starLevel = CalculateStarLevel();
            dialogueText = GetFinishDialogue(starLevel);
            ShowStars(starLevel);
        }

        SetNPCDialogue();
    }

    private int CalculateStarLevel()
    {
        float currentCargo = (float) playerHUD.finalCargo;
        float maxCargo = (float) playerHUD.maxCargoAmount;
        float cargoPercentage = (currentCargo / maxCargo) * 100f;

        switch (cargoPercentage)
        {
            case > 50f:
                return 3;
            case > 25f:
                return 2;
            default:
                return 1;
        }
    }

    private string GetFinishDialogue(int starLevel)
    {
        return starLevel switch
        {
            3 => npcDialogueData.ThreeStarText,
            2 => npcDialogueData.TwoStarText,
            1 => npcDialogueData.OneStarText,
            _ => "I think you did great?"
        };
    }

    private string GetMissionDialogue()
    {

        return npcDialogueData.MissionText;
    }

    private void SetNPCDialogue()
    {
        dialogueUI.SetCharacterSprite(npcDialogueData.Sprite);
        dialogueUI.SetDialogueText(dialogueText);   
    }

    public void ShowDialogue()
    {
        InitDialogues();
        dialogueUI.gameObject.SetActive(true);
       // Time.timeScale = 0;
    }

    public void ShowStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].gameObject.SetActive(true);    
        }
    }

    public void HideDialogue() { dialogueUI.gameObject.SetActive(false); }
}
