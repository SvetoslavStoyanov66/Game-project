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
            if (Input.GetKeyDown(KeyCode.E) && selection.activeSelf)
            {
                player.SleepingOnBed();
                darkingAnimator.StartDarkenAnimation();
                StartCoroutine(Sleeping());
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
            notifierText.text = "Натисни Е за да легнеш на леглото";
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
    IEnumerator Sleeping()
    {
        CharacterController playerController = FindObjectOfType<Player>().GetComponent<CharacterController>();

                if (playerController != null)
                {
                    Quaternion newRotation = Quaternion.Euler(0, 180, 0);
                    player.transform.rotation = newRotation;
                    StartCoroutine(player.DiseableMovement(3));
                    playerController.enabled = false; // Disable the CharacterController temporarily
                    Vector3 newPosition = playerController.transform.position;
                    newPosition = this.gameObject.transform.position;
                    newPosition.z -= 0.5f;
                    newPosition.x += 0.1f;
                    newPosition.y += 0.07f;
                    playerController.transform.position = newPosition;
                    
                    animator.BedSleeping();
                    yield return new WaitForSeconds(3);
                    playerController.enabled = true;
                }
    }
}

