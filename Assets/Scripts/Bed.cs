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
    private void Start()
    {
        time = FindObjectOfType<Timer>();   
        player = FindObjectOfType<Player>();
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
