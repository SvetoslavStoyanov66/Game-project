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
    private bool hasSeedPlanted = false;
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
    public bool isCropInstantianted = false;
    public bool isRaining;
    private float counter;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Soil);
    }
    private void Update()
    {
        if (isRaining)
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

    public void LoadLandData(LandStatus statusToSwich, bool wasWatared)
    {
        landStatus = statusToSwich;
        isWatered = wasWatared;

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
    public void LoadCropData(int id,GameObject seed,GameObject seed1,GameObject seed2,GameObject grownCrop)
    {
        this.id = id;
        this.seed = seed;
        this.seed1 = seed1;
        this.seed2 = seed2;
        this.GrownCrop = grownCrop;

        Vector3 position = this.gameObject.transform.position;
        position.y = 0;
        if (seed != null)
        {
            if (seed.tag == "Potato")
            {
                position.z += -.2f;
            }
            Quaternion desiredRotation = Quaternion.Euler(-90f, 0f, 0f);
            seed = Instantiate(seed, position, desiredRotation);           
        }
        else if (seed1 != null)
        {
            seed1 = Instantiate(seed1, position, Quaternion.identity);          
        }
        else if (seed2 != null)
        {
            seed2 = Instantiate(seed2, position, Quaternion.identity);
        }
        else if (grownCrop != null)
        {
            grownCrop = Instantiate(grownCrop, position, Quaternion.identity);
        }

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
    public void Grow()
    {
        if (grow == true && wasWateredYesterday == true)
        {
            CurrentDayProgression++;
            if (DaysToGrowPorgression != null)
            {
                if (CurrentDayProgression >= DaysToGrowPorgression)
                {
                    Vector3 position = this.gameObject.transform.position;
                    position.y = 0;
                    if (seed1 != null && seed != null)
                    {
                        seed1 = Instantiate(seed1, position, Quaternion.identity);
                        Destroy(seed);
                    }
                    else if (seed1 != null && seed2 != null)
                    {
                        seed2 = Instantiate(seed2, position, Quaternion.identity);
                        Destroy(seed1);
                    }
                    else if (seed2 != null && GrownCrop != null)
                    {
                        GrownCrop = Instantiate(GrownCrop, position, Quaternion.identity);
                        isCropInstantianted = true;
                        Destroy(seed2);
                    }
                    grow = false;
                    wasWateredYesterday = false;
                    CurrentDayProgression = 0;
                }
            }



        }
    }
}

