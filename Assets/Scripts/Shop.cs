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
    GameObject shopUI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
        {
            shopUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E) && shopUI.activeSelf)
        {
            shopUI.SetActive(false);
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
