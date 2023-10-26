using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    [SerializeField]
    GameObject selection;
    Timer time;
    Player player;
    Darking darkingAnimator;
    AnimaationsPlayer animator;
    [SerializeField]
    GameObject notifier;
    Text notifierText;

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
            if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf /*&& ((time.hours > 14 || (time.hours < 6 && time.hours > 0)) || player.fillAmount < 0.3f)*/)
            {

                player.SleepingOnBed();
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
            notifier.SetActive(true);
            notifierText = notifier.GetComponentInChildren<Text>();
            notifierText.text = "Press E to go to bed";
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
