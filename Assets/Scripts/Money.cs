﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public static Money Instance { get; set; }
    [SerializeField]
    Text moneyText;
    public int moneyAmount;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        moneyText = GetComponent<Text>();
        moneyAmount = 2000;
    }

    
    void Update()
    {
        moneyText.text = moneyAmount.ToString();
    }
    public void BuingItems(int priece)
    {
        moneyAmount -= priece;
    }
}
