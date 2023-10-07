using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
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
    public GameObject seed;
    public GameObject seed1;
    public GameObject seed2;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Soil);
    }

   
    
    public void SwitchLandStatus(LandStatus statusToSwich)
    {

        landStatus = statusToSwich;
        if (landStatus == LandStatus.Soil && isWatered == true)
        {
            wasWateredYesterday = true;
           
            Vector3 position = this.gameObject.transform.position;
            position.y = 0;
            if (seed1 != null && seed != null)
            {
                Instantiate(seed1, position, Quaternion.identity);
                Destroy(seed);
            }
            else if (seed1 != null && seed2 != null)
            {
                Instantiate(seed2, position, Quaternion.identity);
                Destroy(seed1);
            }
        }

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
            Debug.Log("Land has been changed to farmland.");
            
        }
        else if (selectedItemName.Equals("Wateringcan"))
        {
            SwitchLandStatus(LandStatus.Watared);
            isWatered = true;
            Debug.Log("Land has been changed to watared.");
          
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
}

