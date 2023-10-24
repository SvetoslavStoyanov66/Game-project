using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownEnter : MonoBehaviour
{
    [SerializeField]
    GameObject selection;
    [SerializeField]
    GameObject townEnter;
    [SerializeField]
    GameObject farmEnter;
    [SerializeField]
    GameObject farm;
    [SerializeField]
    GameObject town;
    
    
    void Update()
    {
        if (selection.activeSelf && Input.GetKeyDown(KeyCode.E) && farm.activeSelf)
        {
            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            if (playerController != null)
            {
                playerController.enabled = false; 
                Vector3 newPosition = townEnter.transform.position;
                playerController.transform.position = newPosition;
                playerController.enabled = true;
                selection.SetActive(false);
                farm.SetActive(false);
                town.SetActive(true);
                this.gameObject.transform.position = newPosition;
            }
        }
        else if (selection.activeSelf && Input.GetKeyDown(KeyCode.E) && !farm.activeSelf)
        {
            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            playerController.enabled = false;
            Vector3 newPosition = farmEnter.transform.position;
            playerController.transform.position = newPosition;
            playerController.enabled = true;
            selection.SetActive(false);
            town.SetActive(false);
            farm.SetActive (true);
            this.gameObject.transform.position = newPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        selection.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        selection.SetActive(false);
    }
}
