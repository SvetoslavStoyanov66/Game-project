using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterDoor : MonoBehaviour
{
    [SerializeField]
    GameObject selection;
    [SerializeField]
    public GameObject house;
    [SerializeField]
    public GameObject inHouse;
    [SerializeField]
    public Camera inHouseCamera;
    [SerializeField]
    GameObject outOfTheHouse;
    [SerializeField]
    Light homeLight;
    [SerializeField]
    Light sun;
    [SerializeField]
    GameObject notifier;
    Text notifiText;
    [SerializeField]
    Transform inHouseTransform;
    


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf && house.activeSelf && !inHouse.activeSelf)
        {
            house.SetActive(false);
            inHouse.SetActive(true);
            selection.SetActive(false);
            notifier.SetActive(false);
            inHouseCamera.enabled = true;
            outOfTheHouse.SetActive(true);
            homeLight.enabled = true;
            sun.enabled = false;
            

            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            if (playerController != null)
            {

                playerController.enabled = false; 
                Vector3 newPosition = inHouseTransform.position;     
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
    public void StructureAssigment(GameObject outside, GameObject inside)
    {
        house = outside;
    }
}
