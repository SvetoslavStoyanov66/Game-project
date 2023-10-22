using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField]
    Text moneyText;
    public int moneyAmount;
    void Start()
    {
        moneyText = GetComponent<Text>();
        moneyAmount = 500;
    }

    
    void Update()
    {
        moneyText.text = moneyAmount.ToString();
    }
}
