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
            ToggleShopUI();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {          
            if (shopUI.enabled)
            {
                UImanager.Instance.ToggleInventoryPanel(69);
            }
            shopUI.enabled = false;
        }
    }

    private void ToggleShopUI()
    {
        isShopOpen = !isShopOpen;
        shopUI.enabled = isShopOpen;
        UImanager.Instance.ToggleInventoryPanel(-189);
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
