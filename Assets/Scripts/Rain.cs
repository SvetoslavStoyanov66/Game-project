using System.Collections.Generic;
using UnityEngine;
using static Land;

public class Rain : MonoBehaviour
{
    public bool isRaining = false;
    public void EnteringBuildingWhileRaining()
    {
        if(isRaining)
        {
            GameObject particleSystem = gameObject.transform.GetChild(0).gameObject;
            particleSystem.SetActive(false);
        }
    }
    public void ExitingBuildingWhileRaining()
    {
        if(isRaining)
        {
            GameObject particleSystem = gameObject.transform.GetChild(0).gameObject;
            particleSystem.SetActive(true);
        }
    }
}
