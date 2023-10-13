using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    public LandManager Instance;
    [SerializeField]
    List<Land> LandList = new List<Land>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        RegisterLandPlots(); 
    }
    private void RegisterLandPlots()
    {
         foreach (Transform areaTranform in transform)
        {
            foreach (Transform landTransform in areaTranform)
            {
                Land land = landTransform.GetComponent<Land>();
                LandList.Add(land);
                land.id = LandList.Count - 1;
            }
        }
    }
}
