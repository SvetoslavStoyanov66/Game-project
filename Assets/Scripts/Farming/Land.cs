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

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Soil);
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
            Debug.Log("Land has been changed to watared.");
        }
        else
        {
            Debug.Log("Selected item is not a hoe. Land state remains unchanged.");
        }
    }
   
}

