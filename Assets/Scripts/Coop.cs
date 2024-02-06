using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Coop : MonoBehaviour
{
    [SerializeField]
    Transform[] spownPoints = new Transform[5];
    [SerializeField]
    GameObject[] chickenBorders = new GameObject[5];
    [SerializeField]
    Timer timer;    
    [SerializeField]
    Text aveibleEggsText;
    public void SpownChicken(int id,GameObject prefab,string name)
    {
        
        if(id <= 5)
        {
            Chiken chicken = new Chiken(id,prefab,name);
            prefab = Instantiate(prefab, spownPoints[id - 1].position ,Quaternion.identity);
            chickenBorders[id - 1].SetActive(true);
            AssignAnimalUI(chicken,chickenBorders[id - 1]);
            AnimalMovement animal = prefab.GetComponent<AnimalMovement>();
            animal.AssignUI(name);
            timer.chickens.Add(animal);
        }

    }
    private void AssignAnimalUI(Chiken chiken,GameObject uiBox)
    {
        Text nameText = uiBox.transform.GetChild(1).GetComponent<Text>();
        nameText.text = chiken.Name;
    }
    public void EggTextValueAssigning(int value)
    {
        int currentEggs = Convert.ToInt32(aveibleEggsText.text);
        currentEggs += value;
        aveibleEggsText.text = currentEggs.ToString();
    }
}

class Chiken
    {
        public int ID { get; set; }
        public GameObject PreFab { get; set; }

        public string Name {get; set;}
        public Chiken(int id, GameObject prefab,string name)
        {
            ID = id;
            PreFab = prefab;
            Name = name;
        }

    }