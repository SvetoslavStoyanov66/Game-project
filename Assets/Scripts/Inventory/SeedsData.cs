using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedsData : ItemData
{
    public GameObject seedling1;
    public GameObject seedling2;
    public int daysToGrow;
    public ItemData cropToYield;
    public int priece;
}
