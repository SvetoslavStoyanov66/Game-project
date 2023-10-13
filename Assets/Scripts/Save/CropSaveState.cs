using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CropSaveState
{
    public int LandId;
    public GameObject seed;
    public GameObject seed1;
    public GameObject seed2;
    public GameObject grownCrop;

    public CropSaveState(int LandId, GameObject seed,GameObject seed1 , GameObject seed2, GameObject grownCrop)
    {
        this.LandId = LandId;
        this.seed = seed;
        this.seed1 = seed1;
        this.seed2 = seed2;
        this.grownCrop = grownCrop;
    }
}
