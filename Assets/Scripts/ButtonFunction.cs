using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    [SerializeField]
    Image hoverImage;
    [SerializeField]
    GameObject CarNotifierUI;
    void Start()
    {
        hoverImage.enabled = false;
    }

    public void OnHover()
    {
        hoverImage.enabled = true;
    }

    public void OnHoverexit()
    {
        hoverImage.enabled = false;
    }
    public void OnLeaveClick() 
    {
        CarNotifierUI.SetActive(false);
    }

}
