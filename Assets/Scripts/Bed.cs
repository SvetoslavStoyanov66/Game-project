using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour
{
    [SerializeField]
    GameObject selection;
    Timer time;
    Player player;
    Darking darkingAnimator;
    AnimaationsPlayer animator;
    private void Start()
    {
        time = FindObjectOfType<Timer>();   
        player = FindObjectOfType<Player>();
        darkingAnimator = FindObjectOfType<Darking>();
        animator = FindObjectOfType<AnimaationsPlayer>();
    }
    private void Update()
    {
               
        if (time != null && player != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf && ((time.hours > 14 || (time.hours < 6 && time.hours > 0)) || player.fillAmount < 0.3f))
            {
                time.day += 1;
                time.hours = 8;
                player.fillAmount = 1;
                darkingAnimator.StartDarkenAnimation();
                CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

                if (playerController != null)
                {
                    playerController.enabled = false; // Disable the CharacterController temporarily
                    Vector3 newPosition = playerController.transform.position;
                    newPosition = this.gameObject.transform.position;
                    newPosition.z -= 0.5f;
                    newPosition.x += 0.1f;
                    playerController.transform.position = newPosition;
                    Quaternion newRotation = Quaternion.Euler(0, 180, 0);
                    player.transform.rotation = newRotation;
                    StartCoroutine(player.DiseableMovement(3));
                    playerController.enabled = true;               
                    animator.BedSleeping();
                }
            }
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
