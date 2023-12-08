using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using UnityEngine;

public class Coop : MonoBehaviour
{
    [SerializeField]
    Transform[] spownPoints = new Transform[5];
    [SerializeField]
    GameObject animalStatsBox;
    [SerializeField]
    GameObject animalNotifierBox;
    public void SpownChicken(int id,GameObject prefab,string name)
    {
        
        if(id <= 5)
        {
            new Chiken(id,prefab,name);
            prefab = Instantiate(prefab, spownPoints[id - 1].position ,Quaternion.identity);
            AnimalMovement animal = prefab.GetComponent<AnimalMovement>();
            animal.AssignUI(animalStatsBox, animalNotifierBox, name);
        }

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