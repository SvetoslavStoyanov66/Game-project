using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Land;

public class LandManager : MonoBehaviour
{
    public  static LandManager Instance;

    public static Tuple<List<LandSaveState>, List<CropSaveState>> farmData = null;

    [SerializeField]
    List<Land> LandList = new List<Land>();
    [SerializeField]
    List<LandSaveState>  landSaveState = new List<LandSaveState>();
    [SerializeField]
    public List<CropSaveState> cropSaveState = new List<CropSaveState>();
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
    
    private void OnEnable()
    {
        RegisterLandPlots();
        StartCoroutine(LoadFarmData());
        
    }
    IEnumerator LoadFarmData()
    {
        yield return new WaitForEndOfFrame();
        if (farmData != null)
        {
            ImportLandData(farmData.Item1);
            ImportCropData(farmData.Item2);
        }
    }
    private void OnDestroy()
    {
        farmData = new Tuple<List<LandSaveState>, List <CropSaveState>> (landSaveState,cropSaveState);
    }
    private void RegisterLandPlots()
    {
        foreach (Transform areaTranform in transform)
        {
            foreach (Transform landTransform in areaTranform)
            {
                Land land = landTransform.GetComponent<Land>();
                LandList.Add(land);

                landSaveState.Add(new LandSaveState());
                cropSaveState.Add(new CropSaveState());
                land.id = LandList.Count - 1;
            }
        }
    }
    
    public void ImportLandData(List<LandSaveState> landDataSetToLoad)
    {
        for (int i = 0; i < landDataSetToLoad.Count; i++)
        {
            
            LandSaveState landDataToLoad = landDataSetToLoad[i];
            LandList[i].LoadLandData(landDataToLoad.landStatus, landDataToLoad.lastWatered);
        }
        landSaveState = landDataSetToLoad; 
    }
    public void ImportCropData(List<CropSaveState> cropDataSetToLoad)
    {
        for (int i = 0; i < cropDataSetToLoad.Count; i++)
        {

            CropSaveState cropDataToLoad = cropDataSetToLoad[i];
            LandList[i].LoadCropData(cropDataToLoad.LandId, cropDataToLoad.seed, cropDataToLoad.seed1,cropDataToLoad.seed2, cropDataToLoad.grownCrop);
        }
        cropSaveState = cropDataSetToLoad;
    }
    public void OnLandChange(int id, Land.LandStatus landStatus, bool lastWatared)
    {
        landSaveState[id] = new LandSaveState(landStatus, lastWatared);
    }
    public void OnCropChange(int id, GameObject seed, GameObject seed1, GameObject seed2, GameObject grownCrop)
    {
        cropSaveState[id] = new CropSaveState(id, seed, seed1, seed2, grownCrop);
    }
}
