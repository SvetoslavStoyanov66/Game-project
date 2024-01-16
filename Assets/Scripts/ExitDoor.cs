using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitDoor : MonoBehaviour
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
    [SerializeField]
    Transform enterDoorTransform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf && !house.activeSelf && inHouse.activeSelf)
        {
            selection.SetActive(false);
            notifier.SetActive(false);
            house.SetActive(true);
            inHouse.SetActive(false);
            inHouseCamera.enabled = false;
            outOfTheHouse.SetActive(false);
            homeLight.enabled = false;
            sun.enabled = true;

            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            if (playerController != null)
            {
                playerController.enabled = false;
                Vector3 newPosition = enterDoorTransform.position;
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
            Text notifiText = notifier.GetComponentInChildren<Text>();
            notifiText.text = "Press E to leave";
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
        inHouse = inside;
    }
}
