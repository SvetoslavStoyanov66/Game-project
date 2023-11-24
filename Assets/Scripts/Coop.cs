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
    public void SpownChicken(int id,GameObject prefab,string name)
    {
        new Chiken(id,prefab,name);
        if(id <= 5)
        {
            prefab = Instantiate(prefab, spownPoints[id - 1].position ,Quaternion.identity);
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