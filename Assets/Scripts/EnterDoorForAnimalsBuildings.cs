using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterDoorForAnimalsBuildings : MonoBehaviour
{
    
    GameObject selection;
    
    Camera inHouseCamera;

    Light homeLight;
    Light sun;
    GameObject notifier;
    Text notifiText;
    
    GameObject door;

    [SerializeField]

    Canvas coopCanvas;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
        {
            MainCamera mainCamera = FindObjectOfType<MainCamera>();
            mainCamera.PlayerFollowing(false);
            selection.SetActive(false);
            notifier.SetActive(false);
            inHouseCamera.enabled = true;
            homeLight.enabled = true;
            sun.enabled = false;
            coopCanvas.enabled = true;

            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            if (playerController != null)
            {
                playerController.enabled = false;  
                Vector3 newPosition = door.transform.position;
                playerController.transform.position = newPosition;
                playerController.enabled = true;
            }
        }      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selection.SetActive(true);
            notifier.SetActive(true);
            notifiText = notifier.GetComponentInChildren<Text>();
            notifiText.text = "Натисни Е за да влезеш";
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
    public void Assigment(GameObject selection,Camera inHouseCamera,Light homeLight,Light sun,GameObject notifier,GameObject door,Canvas canvas)
    {
        this.selection = selection;
        this.inHouseCamera = inHouseCamera;
        this.homeLight = homeLight;
        this.sun = sun;
        this.notifier = notifier;
        this.door = door;
        this.coopCanvas = canvas;
    }
}
