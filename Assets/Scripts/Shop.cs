using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }
    [SerializeField]
    GameObject selection;
    [SerializeField]
    Canvas shopUI;
    public bool isShopOpen = false;
    [SerializeField]
    Button invButton;
    bool isInventoryActive;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
        {
            if (shopUI.enabled)
            {
                UImanager.Instance.inventoryPanel.SetActive(false);
                shopUI.enabled = false;
                invButton.gameObject.SetActive(true);
            }
            else
            {
                shopUI.enabled = true;
                ToggleShopUI();
                invButton.gameObject.SetActive(false);           
            }         
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (shopUI.enabled)
            {
                UImanager.Instance.inventoryPanel.SetActive(false);
            }
            shopUI.enabled = false;
            invButton.gameObject.SetActive(true);
        }
        isShopOpen = shopUI.enabled;
        
    }

    private void ToggleShopUI()
    {
       
        if (!UImanager.Instance.IsInventoryPanelActive())
        {
            UImanager.Instance.ToggleInventoryPanel(-189);
        }
        else
        {
            UImanager.Instance.ChangeInventoryPosition(-189);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selection.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selection.SetActive(false);
        }
    }
}
