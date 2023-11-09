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
    [SerializeField]
    Camera camera;
    
    
   public void Transporting()
   {
    if (farm.activeSelf)
        {
            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            if (playerController != null)
            {
                playerController.enabled = false; 
                Vector3 newPosition = townEnter.transform.position;
                playerController.transform.position = newPosition;
                playerController.enabled = true;
                StartCoroutine(TurnOffObject(farm));
                town.SetActive(true);
                this.gameObject.transform.position = newPosition;
                //camera.transform.position = newPosition;  
            }
        }
        else if (!farm.activeSelf)
        {
            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            playerController.enabled = false;
            Vector3 newPosition = farmEnter.transform.position;
            playerController.transform.position = newPosition;
            playerController.enabled = true;
            StartCoroutine(TurnOffObject(town));
            farm.SetActive (true);
            this.gameObject.transform.position = newPosition;
            //camera.transform.position = newPosition;
        }
   }
   IEnumerator TurnOffObject(GameObject objectToTurnOff)
   {
      yield return new WaitForSeconds(3);
      objectToTurnOff.SetActive(false);
   }
}
