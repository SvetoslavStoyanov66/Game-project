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
    Camera maincamera;
    
    
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
                StartCoroutine(TurnOffObject(farm, 3));
                town.SetActive(true);
                this.gameObject.transform.position = newPosition;  
            }
        }
    else if (!farm.activeSelf)
        {
            CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

            playerController.enabled = false;
            Vector3 newPosition = farmEnter.transform.position;
            playerController.transform.position = newPosition;
            playerController.enabled = true;
            StartCoroutine(TurnOffObject(town, 3f));
            farm.SetActive (true);
            this.gameObject.transform.position = newPosition;
        }
   }
   IEnumerator TurnOffObject(GameObject objectToTurnOff, float time)
   {
     CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();
      yield return new WaitForSeconds(time);
      objectToTurnOff.SetActive(false);
      Vector3 desiredPosition = playerController.transform.position;
      desiredPosition.y += 6;
      desiredPosition.z -= 9;
      maincamera.transform.position = desiredPosition;
   }
}
