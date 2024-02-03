using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitDoorForAnimalsBuildings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject selection;
    [SerializeField]
    Camera inHouseCamera;
    [SerializeField]
    Light homeLight;
    [SerializeField]
    Light sun;
    [SerializeField]
    GameObject notifier;
    GameObject door;

    [SerializeField]
   
    Canvas animalCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
        {
            MainCamera mainCamera = FindObjectOfType<MainCamera>();
            mainCamera.PlayerFollowing(true);
            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            if (playerController != null)
            {
                if(door != null)
                {
                    playerController.enabled = false; // Disable the CharacterController temporarily
                Vector3 newPosition = door.transform.position;
                playerController.transform.position = newPosition; // Set the new position
                playerController.enabled = true;   
                }            
            }
            selection.SetActive(false);
            notifier.SetActive(false);
            inHouseCamera.enabled = false;
            homeLight.enabled = false;
            sun.enabled = true;
            animalCanvas.enabled = false;       
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selection.SetActive(true);
            notifier.SetActive(true);
            Text notifiText = notifier.GetComponentInChildren<Text>();
            notifiText.text = "Натисни Е за да излезеш";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selection.SetActive(false);
            notifier.SetActive(false);
        }
    }
     public void DoorAssigment(GameObject door)
    {
        this.door = door;
    }
}
