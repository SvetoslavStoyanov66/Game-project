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
    enum animal
    {
        Cow,
        Chicken
    }
    [SerializeField]
    private animal animalType;
    void Start()
    {
        interaction = FindObjectOfType<PlayerInteraction>();
        fullFeeder = this.gameObject.transform.GetChild(0).gameObject;
        emptyFeeder = this.gameObject.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isTrue && !fullFeeder.activeSelf && ((interaction.isChickenFoodSelected() && animalType == animal.Chicken) || (interaction.isCowFoodSelected() && animalType == animal.Cow)))
        {
            emptyFeeder.SetActive(false);
            fullFeeder.SetActive(true);
            interaction.FoodIremReduction();
        }
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            notifer.SetActive(true);
            isTrue = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
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
    public bool isFull()
    {
        return fullFeeder.activeSelf;    
    }
}
