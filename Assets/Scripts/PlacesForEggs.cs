
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class PlacesForEggs : MonoBehaviour
{
    [SerializeField]
    ItemData eggData;
    [SerializeField]
    GameObject notifier;
    [SerializeField]
    GameObject eggPrefab;
    GameObject egg;
    
    bool isTrue;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isTrue)
        {
            Inventory.Instance.HarvestCrops(eggData);
            Destroy(egg);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && egg != null)
        {
            isTrue = true;
            notifier.SetActive(true);
        }     
    }
    private void OnTriggerExit(Collider other) 
    { 
        if(other.CompareTag("Player"))
        {
            isTrue = false;
            notifier.SetActive(false);
        }     
    }
    public void SpownEgg()
    {
        egg = Instantiate(eggPrefab,this.gameObject.transform.position,Quaternion.identity);
    }
    public bool isThereEgg()
    {
        if(egg != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
