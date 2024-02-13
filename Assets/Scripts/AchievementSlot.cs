using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
    ItemData item;
    public Text nameText;
    Image picture;
    [SerializeField]
    Image itemInformationWindow;
    Image exclamationMark;
    public bool exclamationMarkNeed = true;

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
                exclamationMark = this.gameObject.transform.GetChild(2).GetComponent<Image>();
                if(exclamationMarkNeed)
                {
                    exclamationMark.gameObject.SetActive(true);
                }
                
            }
            else
            {
                nameText.text = "???";
                picture.sprite = item.UnUnlockedThubnail;
            }
        }      
    }
    public void ShowItemInformationWindow()
    {
        if(item.achievementUnlock)
        {

            itemInformationWindow.gameObject.SetActive(true);
            Text name = itemInformationWindow.transform.GetChild(0).GetComponent<Text>();
            Transform scrollViewTransform = itemInformationWindow.transform.GetChild(1);
            Transform viewPortTransform = scrollViewTransform.GetChild(0);
            Text discription = viewPortTransform.GetChild(0).GetComponent<Text>();
            Image image = itemInformationWindow.transform.GetChild(2).GetComponent<Image>();
            Animator animationImage = itemInformationWindow.transform.GetChild(4).GetComponent<Animator>();
            if (animationImage.HasState(0, Animator.StringToHash(item.name + "Animation")))
            {
                animationImage.Play(item.name + "Animation");
            }

            exclamationMark.gameObject.SetActive(false);
            exclamationMarkNeed = false;

            AchievementManager.Instance.ApplyExclamationMarkNotfier();
            name.text = item.bulgarianName;
            
            discription.text = item.achievementDiscription;
            image.sprite = item.thumbnail;
        }

    }
    public bool isExlamactionMarkActive()
    {
        if(item != null && item.achievementUnlock)
        {
            return exclamationMarkNeed;
        }
        else
        {
            return false;
        }
    }
}
