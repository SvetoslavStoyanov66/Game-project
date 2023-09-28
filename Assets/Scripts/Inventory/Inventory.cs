using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    private bool initialVisibility = true;
    public GameObject inventory;

    [Header("Tools")]
    public ItemData[] Tools = new ItemData[8];
    public ItemData equippedTool = null;


    [Header("Items")]
    public ItemData[] Items = new ItemData[8];
    public ItemData eqquipedItem = null;

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
   

  
}
