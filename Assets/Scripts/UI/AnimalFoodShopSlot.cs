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
    [SerializeField]
    Image clickImage;
    [SerializeField]
    Text notEnoughMoneyNot;
    private void Start() {
        Transform iconBorderTransform = this.gameObject.transform.GetChild(0);
        Image image = iconBorderTransform.gameObject.GetComponent<Image>();
        image.sprite = animalFoodData.thumbnail;
    }

    public void PurchaseItem()
    {
        StartCoroutine(clickAnimation());
        if (Money.Instance.moneyAmount > 0 && Money.Instance.moneyAmount - priece >= 0)
        {
            Money.Instance.BuingItems(priece);
            notEnoughMoneyNot.text = "";
            Inventory.Instance.HarvestCrops(animalFoodData);
        }
        else 
        {
            StartCoroutine(TextDuration(4));
        }
    }
    IEnumerator clickAnimation()
    {
        clickImage.enabled = true;
        yield return new WaitForSeconds(0.1f);
        clickImage.enabled = false;
    }
    IEnumerator TextDuration(int num)
    {
        notEnoughMoneyNot.text = "Not enough money";
        yield return new WaitForSeconds(num);
        notEnoughMoneyNot.text = "";
    }
}
