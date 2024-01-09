using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    ItemData item;
    Text nameText;
    Image picture;
    public void ItemAssigning(ItemData data)
    {
        item = data;
    }
    public void UpdateUI()
    {
        if(item != null)
        {
            Transform transform1 = this.gameObject.transform.GetChild(1);
            Transform transform2 = this.gameObject.transform.GetChild(0);
            nameText = transform1.GetComponent<Text>();
            picture = transform2.GetComponent<Image>();
            if(item.achievementUnlock == true)
            {
                nameText.text = item.name;
                picture.sprite = item.thumbnail;
            }
            else
            {
                nameText.text = "???";
                picture.sprite = item.UnUnlockedThubnail;
            }
        }      
    }
}
