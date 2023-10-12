using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject selection;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
        {
            SceneManager.LoadScene("House");
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
