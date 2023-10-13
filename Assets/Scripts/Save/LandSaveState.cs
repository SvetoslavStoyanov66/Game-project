using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct LandSaveState
{
    public Land.LandStatus landStatus;
    public bool lastWatered;

    public LandSaveState(Land.LandStatus landStatus, bool lastWatared)
    {
        this.landStatus = landStatus;
        this.lastWatered = lastWatared;
    }

}
