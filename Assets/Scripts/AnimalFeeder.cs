using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFeeder : MonoBehaviour
{
    PlayerInteraction interaction;
    [SerializeField]
    GameObject notifer;
    bool isTrue = false;
    [SerializeField]
    GameObject fullFeeder;
    [SerializeField]
    GameObject emptyFeeder;
    void Start()
    {
        interaction = FindObjectOfType<PlayerInteraction>();
        fullFeeder = this.gameObject.transform.GetChild(0).gameObject;
        emptyFeeder = this.gameObject.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isTrue && !fullFeeder.activeSelf)
        {
            emptyFeeder.SetActive(false);
            fullFeeder.SetActive(true);
        }
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "PLayer" && !fullFeeder.activeSelf)
        {
            notifer.SetActive(true);
            isTrue = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PLayer")
        {
            notifer.SetActive(false);
            isTrue = false;
        }
    }
    public void EmptyingFeeder()
    {
        fullFeeder.SetActive(false);
        emptyFeeder.SetActive(true);
    }
}
