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
    public void SpownChicken(int id,GameObject prefab)
    {
        new Chiken(id,prefab);
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
        public Chiken(int id, GameObject prefab)
        {
            ID = id;
            PreFab = prefab;
        }

    }