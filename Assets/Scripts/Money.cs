using System.Collections;
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
        moneyAmount = 200;
        LoadMoneyData();
    }
    private void LoadMoneyData()
    {
        SaveData saveData = SaveSystem.LoadGame();
        if (saveData != null && saveData.moneySaveData != null)
        {
            moneyAmount = saveData.moneySaveData.moneyAmount;
        }
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
