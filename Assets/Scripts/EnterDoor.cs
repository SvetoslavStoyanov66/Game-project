using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject selection;
    [SerializeField]
    GameObject house;
    [SerializeField]
    GameObject inHouse;
    [SerializeField]
    Camera inHouseCamera;
    [SerializeField]
    GameObject outOfTheHouse;
    [SerializeField]
    Light homeLight;
    [SerializeField]
    Light sun;
    [SerializeField]
    GameObject notifier;
    Text notifiText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf && house.activeSelf && !inHouse.activeSelf)
        {
            selection.SetActive(false);
            notifier.SetActive(false);
            house.SetActive(false);
            inHouse.SetActive(true);
            inHouseCamera.enabled = true;
            outOfTheHouse.SetActive(true);
            homeLight.enabled = true;
            sun.enabled = false;
            

            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            if (playerController != null)
            {
                playerController.enabled = false; // Disable the CharacterController temporarily
                Vector3 newPosition = playerController.transform.position;
                newPosition.z += 0.8f;
                newPosition.z += 0.1f;
                playerController.transform.position = newPosition; // Set the new position
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
            notifiText.text = "Press E to enter";
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
}
