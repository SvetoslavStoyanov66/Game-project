using System.Collections.Generic;
using UnityEngine;
using static Land;

public class Rain : MonoBehaviour
{
    [SerializeField]
    List<Land> lands;
    private float timeSinceLastReset = 0f;
    public bool isRaining = false;
    private void Update()
    {
        timeSinceLastReset += Time.deltaTime; // Increment the timer in each frame
        foreach (Land landObg in lands)
        {
            if (timeSinceLastReset >= 10f)
            {
                Debug.Log("Resetting land to Watered");
                landObg.SwitchLandStatus(LandStatus.Watared);
                timeSinceLastReset = 0f;
            }
        }
    }
}
