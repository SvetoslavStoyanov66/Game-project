using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    ItemData itemToDisplay;
    public Image ItemDisplayImage;
    public void Display(ItemData itemToDisplay)
    {
        if (itemToDisplay != null)
        {
            ItemDisplayImage.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;
            ItemDisplayImage.gameObject.SetActive(true);
            return;
        }
        ItemDisplayImage.gameObject.SetActive(false);
    }
}
