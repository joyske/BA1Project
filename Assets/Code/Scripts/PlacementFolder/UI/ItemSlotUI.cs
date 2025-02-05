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
    PlacementManager placementManager;

    [SerializeField]
    Sprite normalBG;

    [SerializeField]
    Sprite highlightedBG;

    private void Awake()
    {
        slotButton = gameObject.GetComponent<Button>();
    }


    /// <summary>
    /// Initializes Data 
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="itemSprite"></param>
    /// <param name="itemQuantity"></param>
    public void SetData(int ID, Sprite itemSprite, int itemQuantity)
    {
        itemID = ID;
        image.sprite = itemSprite;
        image.enabled = true;
        quantity.text = itemQuantity.ToString();

        // Add listener for onClick 
        slotButton.onClick.RemoveAllListeners();
        slotButton.onClick.AddListener(() => placementManager.StartPlacement(itemID));

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

    public void IsSelected(bool selected)
    {
        GetComponent<Image>().sprite = selected ? highlightedBG : normalBG;
    }
}
