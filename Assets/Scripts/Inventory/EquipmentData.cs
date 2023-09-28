using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/EquipmentTool")]

public class EquipmentData : ItemData
{
    public enum ToolType 
    {
        Hoe, WateringCan, Axe, Pickaxe
    }
    public ToolType toolType;

}
