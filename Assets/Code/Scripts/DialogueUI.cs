using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    Image characterImage;

    [SerializeField]
    TMP_Text dialogueText;
    

    public void SetCharacterSprite(Sprite characterSprite)
    {
        characterImage.sprite = characterSprite;    
    }

    public void SetDialogueText(string dialogue)
    {
        dialogueText.text = dialogue;
    }
}
