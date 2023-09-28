using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedsData : ItemData
{
    public int daysToGrow;
    public ItemData cropToYield;
}
