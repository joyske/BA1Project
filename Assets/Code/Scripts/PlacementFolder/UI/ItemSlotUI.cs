using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    int itemID;

    [SerializeField]
    Image image;

    [SerializeField]
    TextMeshProUGUI quantity;

    [SerializeField]
    Button slotButton;

    [SerializeField]
    PlacementSystem placementSystem;

    private void Awake()
    {
        slotButton = gameObject.GetComponent<Button>();
    }

    public void SetData(int ID, Sprite itemSprite, int itemQuantity)
    {
        itemID = ID;
        image.sprite = itemSprite;
        image.enabled = true;
        quantity.text = itemQuantity.ToString();

        // Add listener for onClick 
        slotButton.onClick.RemoveAllListeners();
        slotButton.onClick.AddListener(() => placementSystem.StartPlacement(itemID));

    }

    public void UpdateQuantity(int itemQuantity)
    {
        quantity.text = itemQuantity.ToString();
    }

    public void DisableButton()
    {
        GetComponentInChildren<Button>().interactable = false;
        image.sprite = null;
        quantity.text = null;
    }

    public int GetItemID()
    {
        return itemID;
    }
}
