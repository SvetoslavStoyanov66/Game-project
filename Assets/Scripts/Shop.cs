using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject selection;
    [SerializeField]
    Canvas shopUI;
    private bool isShopOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
        {
            ToggleShopUI();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            shopUI.enabled = false;
        }
    }

    private void ToggleShopUI()
    {
        isShopOpen = !isShopOpen;
        shopUI.enabled = isShopOpen;
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
