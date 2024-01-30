using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimalFoodShopSlot : MonoBehaviour
{
    [SerializeField]
    ItemData animalFoodData;
    [SerializeField]
    int priece;
    private void Start() {
        Transform iconBorderTransform = this.gameObject.transform.GetChild(0);
        Image image = iconBorderTransform.gameObject.GetComponent<Image>();
        image.sprite = animalFoodData.thumbnail;
    }

    public void PurchaseItem()
    {
        Inventory.Instance.HarvestCrops(animalFoodData);
        Money.Instance.BuingItems(priece);
    }
}
