using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public int id;
    public LandStatus landStatus = LandStatus.Soil;
    public enum LandStatus 
    {
        Soil,Farmland,Watared
    }


    public Material soilMat, farmlandMat, wataredMat;
    new Renderer renderer;
    public GameObject select;
    public bool wasWateredYesterday = false;
    public bool isWatered = false;
    public bool hasSeedPlanted = false;
    [SerializeField]
    public GameObject seed;
    [SerializeField]
    public GameObject seed1;
    [SerializeField]
    public GameObject seed2;
    [SerializeField]
    public GameObject GrownCrop;
    public bool grow = false;
    public int DaysToGrowPorgression;
    public int CurrentDayProgression = 0;
    public ItemData crop;
    public SeedsData seedData;
    public bool isCropInstantianted = false;
    public bool isRaining;
    private float counter;

    public bool hasMultyCollectableSeed = false;

    public GameObject harvestedCrop;

    int daysForCollectingMultyHarvestableCrops = 3;
    bool InstantiatedHarvestedCrop = false;
    
      void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(landStatus);
    }
    public void LoadLandState()
    {
        SaveData saveData = SaveSystem.LoadGame(0);
        if (saveData != null && saveData.landsSaveData != null)
        {
            LandSaveData landSaveData = saveData.landsSaveData.Find(x => x.id == this.id);
            if (landSaveData != null)
            {
                this.wasWateredYesterday = landSaveData.wasWateredYesterday;
                this.hasSeedPlanted = landSaveData.hasSeedPlanted;
                this.CurrentDayProgression = landSaveData.currentDayProgression;
                this.isCropInstantianted = landSaveData.isCropInstantiated;
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    if (!string.IsNullOrEmpty(landSaveData.cropDataName) || landSaveData.cropDataName != "NoCrop")
                    {
                        this.crop = gameManager.GetItemDataByName(landSaveData.cropDataName);
                    }

                    if (!string.IsNullOrEmpty(landSaveData.seedDataName) || landSaveData.cropDataName != "NoSeedData")
                    {
                        this.seedData = gameManager.GetItemDataByName(landSaveData.seedDataName) as SeedsData;
                    }
                }

                Quaternion desiredRotation = Quaternion.Euler(0, 0f, 0f);
                Vector3 position = this.gameObject.transform.position;
                position.y = 0.015f;

                if (landSaveData.hasSeedPlanted && !string.IsNullOrEmpty(landSaveData.cropDataName))
                {
                    if (landSaveData.seedExists && crop != null && seedData != null)
                    {
                        desiredRotation = Quaternion.Euler(-90f, 0f, 0f);
                        seed = Instantiate(seedData.gameModel, position, desiredRotation);
                        seed1 = seedData.seedling1;
                        seed2 = seedData.seedling2;
                        GrownCrop = crop.gameModel;
                    }
                    else if (landSaveData.seed1Exists && crop != null && seedData != null)
                    {
                        seed1 = Instantiate(seedData.seedling1, position, desiredRotation);
                        seed2 = seedData.seedling2;
                        GrownCrop = crop.gameModel;
                    }
                    else if (landSaveData.seed2Exists && crop != null && seedData != null)
                    {
                        seed2 = Instantiate(seedData.seedling2, position, desiredRotation);
                        GrownCrop = crop.gameModel;
                    }
                    else if (landSaveData.grownCropExists && crop != null && seedData != null)
                    {
                        GrownCrop = Instantiate(crop.gameModel, position, desiredRotation);
                    }
                }



            }
        }
    }

 
    private void Update()
    {
        if (isRaining && landStatus == LandStatus.Farmland)
        {
            counter += Time.deltaTime;
            if (counter >= 10)
            {
                SwitchLandStatus(LandStatus.Watared);
                counter = 0;
            }
        }
    }


    public void SwitchLandStatus(LandStatus statusToSwich)
    {

        landStatus = statusToSwich;
       

        Material materialTpSwich = soilMat;
        switch (statusToSwich)
        {
            case LandStatus.Soil:
                materialTpSwich = soilMat;
                break;
            case LandStatus.Farmland:
                materialTpSwich = farmlandMat;
                break;
            case LandStatus.Watared:
                materialTpSwich = wataredMat;
                isWatered = true;
                break;
        }
        renderer.material = materialTpSwich;
    }
    public void Selected(bool toggle)
    {
        select.SetActive(toggle);
    }

    public void Interact(string selectedItemName)
    {
        // Change the land state to farmland only if the selected item is a "hoe"
        if (selectedItemName.Equals("Hoe"))
        {
            SwitchLandStatus(LandStatus.Farmland);
            
        }
        else if (selectedItemName.Equals("Wateringcan"))
        {
            SwitchLandStatus(LandStatus.Watared);
          
        }

        else
        {
            Debug.Log("Selected item is not a hoe. Land state remains unchanged.");
        }
    }
    public void ResetToSoil()
    {
        SwitchLandStatus(LandStatus.Soil);
    }
    public void ResetToWatared()
    {
        SwitchLandStatus(LandStatus.Watared);
    }
    public bool HasSeedPlanted()
    {
        return hasSeedPlanted;
    }
    public void PlantSeed()
    {
        hasSeedPlanted = true;
    } 
    public void HarvestSeed()
    {
        hasSeedPlanted = false;
    }
    public void HarvestSeedMultypleColectableSeed()
    {
        if (daysForCollectingMultyHarvestableCrops > 0)
        {
            if(GrownCrop.activeSelf)
            {
                daysForCollectingMultyHarvestableCrops--;
                Inventory.Instance.HarvestCrops(crop);
            }
            
            if (!InstantiatedHarvestedCrop)
            {
                Vector3 position = this.gameObject.transform.position;
                position.y = 0.015f;
                Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                harvestedCrop = Instantiate(harvestedCrop, position, rotation);
                harvestedCrop.SetActive(true);
                InstantiatedHarvestedCrop = true;
                GrownCrop.SetActive(false);

            }
            else
            {
                harvestedCrop.SetActive(true);
                GrownCrop.SetActive(false);
            }

        }
        else
        {
            HarvestSeed();
            Destroy(GrownCrop);
            Destroy(harvestedCrop);
            daysForCollectingMultyHarvestableCrops = 3;
            InstantiatedHarvestedCrop = false;
            isCropInstantianted = false;
        }
    }
    public void Grow()
    {
        if (grow == true && wasWateredYesterday == true)
        {
            CurrentDayProgression++;
            if (DaysToGrowPorgression != null)
            {
                if (CurrentDayProgression >= DaysToGrowPorgression)
                {
                    Quaternion desiredRotation = Quaternion.identity;
                    if(GrownCrop != null && GrownCrop.tag == "seed1")
                    {
                        desiredRotation = Quaternion.Euler(-90f, 0f, 0f);
                    }

                    Vector3 position = this.gameObject.transform.position;
                    position.y = 0.015f;
                    if (seed1 != null && seed != null)
                    {
                        seed1 = Instantiate(seed1, position, desiredRotation);
                        Destroy(seed);
                    }
                    else if (seed1 != null && seed2 != null)
                    {
                        seed2 = Instantiate(seed2, position, desiredRotation);
                        Destroy(seed1);
                    }
                    else if (seed2 != null && GrownCrop != null)
                    {
                        GrownCrop = Instantiate(GrownCrop, position, desiredRotation);
                        isCropInstantianted = true;
                        Destroy(seed2);
                    }
                    else if(harvestedCrop != null && harvestedCrop.activeSelf)
                    {
                        GrownCrop.SetActive(true);
                        harvestedCrop.SetActive(false);
                    }
                    grow = false;
                    wasWateredYesterday = false;
                    CurrentDayProgression = 0;
                }
            }



        }
    }

}

