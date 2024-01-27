using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowShed : MonoBehaviour
{
   [SerializeField]
    Transform[] spownPoints = new Transform[5];
    public void SpownCow(int id,GameObject prefab,string name)
    {
        
        if(id <= 5)
        {
            Cow cow = new Cow(id,prefab,name);
            prefab = Instantiate(prefab, spownPoints[id - 1].position ,Quaternion.identity);
            AnimalMovement animal = prefab.GetComponent<AnimalMovement>();
            animal.AssignUI(name);
        }

    }
}
class Cow
    {
        public int ID { get; set; }
        public GameObject PreFab { get; set; }

        public string Name {get; set;}
        public Cow(int id, GameObject prefab,string name)
        {
            ID = id;
            PreFab = prefab;
            Name = name;
        }

    }
