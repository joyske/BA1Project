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



    /// <summary>
    /// Initializes Dialogue for Scene
    /// </summary>
    void InitDialogues()
    {
        gameManagement = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagement>();

        sceneID = gameManagement.currentLevelIndex;
        npcDialogueData = npcData.NPCDialogueData.Find(data => data.SceneID == sceneID);


        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            dialogueText = GetMissionDialogue();
        }
        else
        {
            playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<PlayerHUD>();
            int starLevel = CalculateStarLevel();
            dialogueText = GetFinishDialogue(starLevel);
            ShowStars(starLevel);
        }

        SetNPCDialogue();
    }


    /// <summary>
    /// Calculates left cargo percentage and star level
    /// </summary>
    /// <returns></returns>
    private int CalculateStarLevel()
    {
        float currentCargo = (float) playerHUD.finalCargo;
        float maxCargo = (float) playerHUD.maxCargoAmount;
        float cargoPercentage = (currentCargo / maxCargo) * 100f;

        switch (cargoPercentage)
        {
            case > 70f:
                return 3;
            case > 40f:
                return 2;
            default:
                return 1;
        }
    }

    /// <summary>
    /// Return Dialogue based on star level
    /// </summary>
    /// <param name="starLevel"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Sets sprite and dialogue text for scene
    /// </summary>
    private void SetNPCDialogue()
    {
        dialogueUI.SetCharacterSprite(npcDialogueData.Sprite);
        dialogueUI.SetDialogueText(dialogueText);   
    }

    public void ShowDialogue()
    {
        InitDialogues();
        dialogueUI.gameObject.SetActive(true);
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
