using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSceneManager : MonoBehaviour
{
    [SerializeField]
    DialogueSystemController dialogueController;

    [SerializeField]
    MenuScript menuScript;

    [SerializeField]
    PlacementHUDManager placementHUD;

    private void Start()
    {
        dialogueController.ShowDialogue();
    }


    public void ShowPlacementScreen()
    {
        dialogueController.HideDialogue();
        placementHUD.gameObject.SetActive(true);
    }
}
