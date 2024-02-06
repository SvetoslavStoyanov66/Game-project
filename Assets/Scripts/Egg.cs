using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField]
    Coop coop;
    private void Start()
    {
        coop = FindObjectOfType<Coop>();
    }
     void OnDestroy()
    {
        coop.EggTextValueAssigning(-1);
    }
}
