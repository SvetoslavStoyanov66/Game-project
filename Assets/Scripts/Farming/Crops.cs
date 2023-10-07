using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Crops : MonoBehaviour
{
    [SerializeField]
    List<Land> lands;
 
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Grow()
    {
        foreach (Land land in lands)
        {
            if (land.HasSeedPlanted())
            {
                if (land.wasWateredYesterday)
                {
                    Debug.LogError("WateredYesterday");
                    
                    if (land.seed != null)
                    {
                        Instantiate(land.seed, land.transform.position, Quaternion.identity);
                        Debug.Log("grow");
                    }
                   
                }
                
            }
        }
    }
}
